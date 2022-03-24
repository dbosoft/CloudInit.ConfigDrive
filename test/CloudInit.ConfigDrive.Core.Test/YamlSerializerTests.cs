using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Dbosoft.CloudInit.ConfigDrive;
using Xunit;

namespace CloudInit.ConfigDrive.Core.Test;

public class YamlSerializerTests
{
    [Fact]
    public async Task SerializesToValidMetadata()
    {
        var serializer = new YamlSerializer();
        var metaData = new Dictionary<string, string>
        {
            { "key1", "value1" },
            { "key2", "value2" }
        };

        await using var resultStream = await serializer.SerializeToYaml(metaData);
        using var streamReader = new StreamReader(resultStream);
        var act = await streamReader.ReadToEndAsync();

        Assert.Equal(@"key1: value1
key2: value2
", act);
    }


    [Fact]
    public async Task SerializesToValidNetworkDataV1()
    {
        var serializer = new YamlSerializer();
        var networkData = new NetworkData(new []
        {
            new { type = "physical", name = "interface0", mac_address = "00:11:22:33:44:55"}
        });

        await using var resultStream = await serializer.SerializeToYaml(networkData);
        using var streamReader = new StreamReader(resultStream);
        var act = await streamReader.ReadToEndAsync();

        Assert.Equal(@"version: 1
config:
- type: physical
  name: interface0
  mac_address: 00:11:22:33:44:55
", act);
    }


    [Fact]
    public async Task SerializesToValidNetworkDataV2()
    {
        var serializer = new YamlSerializer();
        var networkData = new NetworkData(new Dictionary<string, object>
        {
            {"ethernets", new
                {
                    eth0 = new
                    {
                        match = new { macaddress = "00:11:22:33:44:55" },
                        dhcp4 = true
                    }
                }
            }
        });

        await using var resultStream = await serializer.SerializeToYaml(networkData);
        using var streamReader = new StreamReader(resultStream);
        var act = await streamReader.ReadToEndAsync();

        Assert.Equal(@"version: 2
ethernets:
  eth0:
    match:
      macaddress: 00:11:22:33:44:55
    dhcp4: true
", act);
    }

}