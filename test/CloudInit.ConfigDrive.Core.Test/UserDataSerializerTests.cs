using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dbosoft.CloudInit.ConfigDrive;
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

}