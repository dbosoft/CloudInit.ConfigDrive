using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dbosoft.CloudInit.ConfigDrive
{
    public interface IConfigDriveDataSource
    {
        void ValidateNetworkData(NetworkData networkData);
        void ValidateMetaData(IDictionary<string, string> metadata);
        Task<ConfigDriveContent> GenerateConfigDriveContent(
            IDictionary<string, string> metadata, NetworkData? networkData, IEnumerable<UserData> userData,
            GenerateConfigDriveOptions options);
    }
}