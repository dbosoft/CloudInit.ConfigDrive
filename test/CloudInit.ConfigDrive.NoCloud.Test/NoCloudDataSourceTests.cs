using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Dbosoft.CloudInit.ConfigDrive;
using Moq;
using Xunit;

namespace CloudInit.ConfigDrive.Core.NoCloud.Test;

public class NoCloudDataSourceTests
{
    [Theory]
    [InlineData(NetworkDataFormat.V1)]
    [InlineData(NetworkDataFormat.V2)]
    public void AcceptsValidNetworkData(NetworkDataFormat format)
    {
        var ds = new NoCloudDataSource(new NoCloudConfigDriveMetaData(), new YamlSerializer(),
            new UserDataSerializer());

        ds.ValidateNetworkData(new NetworkData(new Dictionary<string, object>()));
        
    }

    [Fact]
    public void AcceptsValidMetaData()
    {
        var ds = new NoCloudDataSource(new NoCloudConfigDriveMetaData(), new YamlSerializer(),
            new UserDataSerializer());

        ds.ValidateMetaData(new Dictionary<string, string>
        {
            {"key", "value"}
        });
        
    }

    [Fact]
    public void ThrowsOnInstanceIdInAdditionalMetadata()
    {
        var ds = new NoCloudDataSource(new NoCloudConfigDriveMetaData(), new YamlSerializer(),
            new UserDataSerializer());

        Assert.Throws<InvalidOperationException>( () => ds.ValidateMetaData(new Dictionary<string, string>
        {
            {"instance-id", "any"}
        }));

    }

    [Fact]
    public void ThrowsOnHostnameInAdditionalMetadata()
    {
        var ds = new NoCloudDataSource(new NoCloudConfigDriveMetaData(), new YamlSerializer(),
            new UserDataSerializer());

        Assert.Throws<InvalidOperationException>(() => ds.ValidateMetaData(new Dictionary<string, string>
        {
            {"local-hostname", "any"}
        }));

    }

    [Fact]
    public async Task GeneratesValidContent()
    {
        var networkData = new NetworkData(new Dictionary<string, object>());
        var metaData = new Dictionary<string, string> { { "key1", "value1" } };
        var userData = new[] { new UserData(UserDataContentType.CloudConfig, "aaa", Encoding.UTF8) };
        var options = new GenerateConfigDriveOptions();

        await using var mockStreamMetaData = new MemoryStream();
        await using var mockStreamNetworkData = new MemoryStream();
        await using var mockStreamUserData = new MemoryStream();


        var yamlSerializerMock = new Mock<IYamlSerializer>();
        yamlSerializerMock.Setup(x => x.SerializeToYaml(networkData)).ReturnsAsync(mockStreamNetworkData);
        yamlSerializerMock.Setup(x => x.SerializeToYaml(
            It.IsAny<Dictionary<string,string>>())).ReturnsAsync(mockStreamMetaData);

        var userDataSerializerMock = new Mock<IUserDataSerializer>();
        userDataSerializerMock.Setup(x => x.SerializeUserData(userData, options.UserData))
            .ReturnsAsync(mockStreamUserData);


        var ds = new NoCloudDataSource(new NoCloudConfigDriveMetaData
            {
                HostName = "unit",
                InstanceId = "test"
            }, yamlSerializerMock.Object, userDataSerializerMock.Object);



        var content = await ds.GenerateConfigDriveContent(metaData, networkData, userData, options);
        Assert.Equal("cidata", content.MediaName);
        Assert.Equal(3, content.Files.Count);

        Assert.Equal("meta-data", content.Files[0].Path);
        Assert.Equal(mockStreamMetaData, content.Files[0].Content);

        Assert.Equal("network-config", content.Files[1].Path);
        Assert.Equal(mockStreamNetworkData, content.Files[1].Content);

        Assert.Equal("user-data", content.Files[2].Path);
        Assert.Equal(mockStreamUserData, content.Files[2].Content);

    }

}