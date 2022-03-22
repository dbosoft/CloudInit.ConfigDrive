using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Dbosoft.CloudInit.ConfigDrive;
using Moq;
using Xunit;

namespace CloudInit.ConfigDrive.Core.Test;

public class ConfigDriveStringWriterTests
{
    [Theory]
    [InlineData("my metadata", null, null, OnlyMetaDataResult)]
    [InlineData("my metadata", "network data", null, MetaAndNetworkDataResult)]
    [InlineData("my metadata", "network data", "user data", AllDataResult)]

    public async Task WritesExpectedString(
        string metadata,
        string networkData,
        string userData,
        string expected)
    {
        var sb = new StringBuilder();
        var writer = new ConfigDriveStringWriter(sb);

        var dsMock = new Mock<IConfigDriveDataSource>();
        dsMock.Setup(x => x.GenerateConfigDriveContent(
                It.IsAny<IDictionary<string, string>>(),
                It.IsAny<NetworkData>(),
                It.IsAny<IEnumerable<UserData>>(),
                It.IsAny<GenerateConfigDriveOptions>()))
            .ReturnsAsync(() =>
            {
                var res = new ConfigDriveContent("mediaName");

                if (metadata != null)
                    res.Files.Add(new ResultFile("meta", new MemoryStream(Encoding.UTF8.GetBytes(metadata))));
               
                if (networkData != null)
                    res.Files.Add(new ResultFile("network", new MemoryStream(Encoding.UTF8.GetBytes(networkData))));
                
                if (userData != null)
                    res.Files.Add(new ResultFile("user", new MemoryStream(Encoding.UTF8.GetBytes(userData))));


                return res;
            });

        var cd = new Dbosoft.CloudInit.ConfigDrive.ConfigDrive(dsMock.Object);

        await writer.WriteConfigDrive(cd);
        Assert.Equal(expected, sb.ToString());

    }

    private const string OnlyMetaDataResult = @"Media name: mediaName
--- file: meta ---
my metadata
";

    private const string MetaAndNetworkDataResult = @"Media name: mediaName
--- file: meta ---
my metadata
--- file: network ---
network data
";

private const string AllDataResult = @"Media name: mediaName
--- file: meta ---
my metadata
--- file: network ---
network data
--- file: user ---
user data
";
}