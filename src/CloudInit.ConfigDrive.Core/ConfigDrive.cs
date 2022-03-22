using System.Collections.Generic;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace Dbosoft.CloudInit.ConfigDrive
{
    public class ConfigDrive : IConfigDrive
    {
        private readonly IConfigDriveDataSource _dataSource;
        private readonly IList<UserData> _userData = new List<UserData>();
        private NetworkData? _networkData;
        private readonly Dictionary<string,string> _metadata = new Dictionary<string, string>();

        public ConfigDrive(IConfigDriveDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        
        public void SetNetworkData(NetworkData networkData)
        {
            _dataSource.ValidateNetworkData(networkData);
            _networkData = networkData;
        }

        public void AddUserData(UserData userData)
        {
            _userData.Add(userData);
        }


        public void AddMetaData(IDictionary<string,string> metadata)
        {
            _dataSource.ValidateMetaData(metadata);

            foreach (var (k, v) in metadata)
            {
                _metadata.Add(k,v);
            }
        }

        public Task<ConfigDriveContent> GenerateContent(GenerateConfigDriveOptions options)
        {
            return _dataSource.GenerateConfigDriveContent(_metadata, _networkData, _userData, options);
        }
    }
}