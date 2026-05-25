using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dbosoft.CloudInit.ConfigDrive;
using MimeKit;
using Xunit;

namespace CloudInit.ConfigDrive.Core.Test;

public class UserDataSerializerTests
{
    [Fact]
    public async Task WritesFixedHeader()
    {
        var serializer = new UserDataSerializer();

        await using var resultStream =
            await serializer.SerializeUserData(Enumerable.Empty<UserData>(), new UserDataOptions
            {
                Base64Encode = false,
                GZip = false
            });

        using var reader = new StreamReader(resultStream);
        var act = await reader.ReadToEndAsync();

        Assert.Equal(@"From nobody Fri Jan  11 07:00:00 1980
Content-Type: multipart/mixed; boundary=""==BOUNDARY==""
MIME-Version: 1.0

".Replace("\r\n", "\n"), act);
    }

    [Fact]
    public async Task WritesUserData()
    {
        var serializer = new UserDataSerializer();


        await using var resultStream =
            await serializer.SerializeUserData(
                new[]
                {
                    new UserData(UserDataContentType.CloudConfig, "some config", Encoding.ASCII)
                }, new UserDataOptions
                {
                    Base64Encode = false,
                    GZip = false
                });

        using var reader = new StreamReader(resultStream);
        var act = await reader.ReadToEndAsync();

        Assert.Equal(@"From nobody Fri Jan  11 07:00:00 1980
Content-Type: multipart/mixed; boundary=""==BOUNDARY==""
MIME-Version: 1.0

--==BOUNDARY==
MIME-Version: 1.0
Content-Type: text/cloud-config; charset=""us-ascii""

some config
--==BOUNDARY==--
".Replace("\r\n", "\n"), act);
    }

    [Fact]
    public async Task Base64EncodesUserData()
    {
        var serializer = new UserDataSerializer();


        await using var resultStream =
            await serializer.SerializeUserData(
                new[]
                {
                    new UserData(UserDataContentType.CloudConfig, "some config", Encoding.ASCII)
                }, new UserDataOptions
                {
                    Base64Encode = true,
                    GZip = false
                });

        using var reader = new StreamReader(resultStream);
        var act = await reader.ReadToEndAsync();

        Assert.Equal(@$"From nobody Fri Jan  11 07:00:00 1980
Content-Type: multipart/mixed; boundary=""==BOUNDARY==""
MIME-Version: 1.0

--==BOUNDARY==
MIME-Version: 1.0
Content-Type: text/cloud-config; charset=""us-ascii""
Content-Transfer-Encoding: base64

{Convert.ToBase64String(Encoding.ASCII.GetBytes("some config"), Base64FormattingOptions.InsertLineBreaks)}
--==BOUNDARY==--
".Replace("\r\n", "\n"), act);

    }


    [Fact]
    public async Task GZipCompressed()
    {
        var serializer = new UserDataSerializer();

        await using var resultStream =
            await serializer.SerializeUserData(Enumerable.Empty<UserData>(), new UserDataOptions
            {
                Base64Encode = false,
                GZip = true
            });

        await using var decompressedStream = new MemoryStream();
        await using var gzipStream = new GZipStream(resultStream, CompressionMode.Decompress);
        await gzipStream.CopyToAsync(decompressedStream);
        decompressedStream.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(decompressedStream);
        
        var act = await reader.ReadToEndAsync();

        Assert.Equal(@"From nobody Fri Jan  11 07:00:00 1980
Content-Type: multipart/mixed; boundary=""==BOUNDARY==""
MIME-Version: 1.0

".Replace("\r\n", "\n"), act);

    }

    [Fact]
    public async Task EndsWithCloseDelimiter()
    {
        var serializer = new UserDataSerializer();

        await using var resultStream =
            await serializer.SerializeUserData(
                new[]
                {
                    new UserData(UserDataContentType.CloudConfig, "some config", Encoding.ASCII)
                }, new UserDataOptions
                {
                    Base64Encode = false,
                    GZip = false
                });

        using var reader = new StreamReader(resultStream);
        var act = await reader.ReadToEndAsync();

        Assert.EndsWith("--==BOUNDARY==--\n", act);
    }

    [Fact]
    public async Task RoundTripsThroughMimeParserWithoutDroppingParts()
    {
        var serializer = new UserDataSerializer();

        await using var resultStream =
            await serializer.SerializeUserData(
                new[]
                {
                    new UserData(UserDataContentType.CloudConfig, "#cloud-config\nfoo: bar", Encoding.ASCII)
                }, new UserDataOptions
                {
                    Base64Encode = false,
                    GZip = false
                });

        var message = await MimeMessage.LoadAsync(resultStream);
        var multipart = Assert.IsType<Multipart>(message.Body);

        Assert.Single(multipart);
        var part = Assert.IsType<TextPart>(multipart[0]);
        Assert.Equal("text/cloud-config", part.ContentType.MimeType);
        Assert.Equal("#cloud-config\nfoo: bar", part.Text.Replace("\r\n", "\n"));
    }

    [Fact]
    public async Task RoundTripsBodyWithColonFirstLine()
    {
        var serializer = new UserDataSerializer();

        // A body whose first line contains a colon must not be mistaken for a header.
        await using var resultStream =
            await serializer.SerializeUserData(
                new[]
                {
                    new UserData(UserDataContentType.CloudConfig, "foo: bar\nbaz: qux", Encoding.ASCII)
                }, new UserDataOptions
                {
                    Base64Encode = false,
                    GZip = false
                });

        var message = await MimeMessage.LoadAsync(resultStream);
        var multipart = Assert.IsType<Multipart>(message.Body);

        Assert.Single(multipart);
        var part = Assert.IsType<TextPart>(multipart[0]);
        Assert.Equal("foo: bar\nbaz: qux", part.Text.Replace("\r\n", "\n"));
    }

}