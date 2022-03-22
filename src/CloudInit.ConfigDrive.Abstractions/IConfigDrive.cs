using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dbosoft.CloudInit.ConfigDrive
{
    public interface IConfigDrive
    {
        Task<ConfigDriveContent> GenerateContent(GenerateConfigDriveOptions options);
        void SetNetworkData(NetworkData networkData);
        void AddUserData(UserData userData);
        void AddMetaData(IDictionary<string,string> metadata);
    }
}