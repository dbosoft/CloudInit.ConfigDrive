using System.Collections.Generic;
using Dbosoft.CloudInit.ConfigDrive;
using Dbosoft.CloudInit.ConfigDrive.Injection;
using Moq;
using Xunit;

namespace CloudInit.ConfigDrive.Core.Test;

public class ConfigDriveBuilderTests
{
    [Fact]
    public void ThrowsIfBuildWithoutDataSource()
    {
        var builder = new ConfigDriveBuilder();
        Assert.Throws<CloudInitConfigurationException>(() => builder.Build());
    }

    [Fact]
    public void BuildWithDataSource()
    {
        var builder = new ConfigDriveBuilder();
        var dsMock = new Mock<IConfigDriveDataSource>();
        dsMock.Setup(x => x.ValidateNetworkData(It.IsAny<NetworkData>())).Verifiable();

        builder.With(dsMock.Object);
        var configDrive = builder.Build();
        
        configDrive.SetNetworkData(new NetworkData( new Dictionary<string, object>()));
        dsMock.Verify();
    }


    [Fact]
    public void HasDefaultRegistrations()
    {
        var container = new Injectionist();
        var _ = new ConfigDriveBuilder(container);
        
        Assert.IsType<YamlSerializer>(container.Get<IYamlSerializer>().Instance);
        Assert.IsType<UserDataSerializer>(container.Get<IUserDataSerializer>().Instance);

    }

    [Fact]
    public void WithCustomRegistration()
    {
        var container = new Injectionist();
        var builder = new ConfigDriveBuilder(container);

        var customType = new CustomType();
        builder.With(ctx => customType);

        Assert.Equal(customType, container.Get<CustomType>().Instance);

    }

    private class CustomType
    {

    }
}