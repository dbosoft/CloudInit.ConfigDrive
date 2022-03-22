using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dbosoft.CloudInit.ConfigDrive;
using Dbosoft.CloudInit.ConfigDrive.Injection;
using Xunit;

namespace CloudInit.ConfigDrive.Core.NoCloud.Test
{
    public class NoCloudExtensionTests
    {
        [Fact]
        public void NoCloudDataSourceRegistered()
        {
            var container = new Injectionist();
            var builder = new ConfigDriveBuilder(container);
            builder.NoCloud(new NoCloudConfigDriveMetaData());

            Assert.IsType<NoCloudDataSource>(container.Get<IConfigDriveDataSource>().Instance);
        }
    }
}
