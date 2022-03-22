using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dbosoft.CloudInit.ConfigDrive;
using Moq;
using Xunit;

namespace CloudInit.ConfigDrive.Core.Test
{
    public class ConfigDriveTests
    {
        [Fact]
        public void ValidateNetworkData()
        {
            var networkData = new NetworkData(new Dictionary<string, object>());

            var dsMock = new Mock<IConfigDriveDataSource>();
            dsMock.Setup(x => x.ValidateNetworkData(networkData)).Verifiable();

            var configDrive = new Dbosoft.CloudInit.ConfigDrive.ConfigDrive(dsMock.Object);
            configDrive.SetNetworkData(networkData);

            dsMock.Verify();
        }

        [Fact]
        public void ValidatesMetaData()
        {
            var metaData = new Dictionary<string, string>
            {
                {"a", "b"}
            };

            var dsMock = new Mock<IConfigDriveDataSource>();
            dsMock.Setup(x => x.ValidateMetaData(metaData)).Verifiable();

            var configDrive = new Dbosoft.CloudInit.ConfigDrive.ConfigDrive(dsMock.Object);
            configDrive.AddMetaData(metaData);

            dsMock.Verify();
        }

        [Fact]
        public void GeneratesContent()
        {
            var metaData = new Dictionary<string, string>();
            var networkData = new NetworkData(new Dictionary<string, object>());
            var userData = new List<UserData>
            {
                new(UserDataContentType.CloudConfig, "", Encoding.UTF8),
                new(UserDataContentType.CloudConfig, "", Encoding.UTF8)
            };

            var options = new GenerateConfigDriveOptions();

            var dsMock = new Mock<IConfigDriveDataSource>();
            dsMock.Setup(x => x.GenerateConfigDriveContent(It.IsAny<IDictionary<string,string>>(), 
                networkData, 
                It.Is<IEnumerable<UserData>>(x=> x.SequenceEqual(userData)), options)).Verifiable();

            var configDrive = new Dbosoft.CloudInit.ConfigDrive.ConfigDrive(dsMock.Object);
            configDrive.AddMetaData(metaData);
            configDrive.SetNetworkData(networkData);

            foreach (var data in userData)
            {
                configDrive.AddUserData(data);
            }

            configDrive.GenerateContent(options);

            dsMock.Verify();
        }
    }
}
