using System;
using System.IO;
using System.Text;
using JetBrains.Annotations;

namespace Dbosoft.CloudInit.ConfigDrive
{
    [PublicAPI]
    public struct NoCloudConfigDriveMetaData
    {
        public string HostName { get; set; }
        public string InstanceId { get; set; }

        public NoCloudConfigDriveMetaData(string hostname)
        {
            HostName = hostname;
            InstanceId = Guid.NewGuid().ToString();
        }

        public NoCloudConfigDriveMetaData(string hostname, string instanceId)
        {
            HostName = hostname;
            InstanceId = instanceId;
        }

    }
}