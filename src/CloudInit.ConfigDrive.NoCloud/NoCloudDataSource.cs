using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Dbosoft.CloudInit.ConfigDrive
{
    public class NoCloudDataSource : IConfigDriveDataSource
    {
        private readonly NoCloudConfigDriveMetaData _metaData;
        private readonly IYamlSerializer _serializer;
        private readonly IUserDataSerializer _userDataSerializer;

        public NoCloudDataSource(
            NoCloudConfigDriveMetaData metaData, 
            IYamlSerializer serializer, 
            IUserDataSerializer userDataSerializer)
        {
            _metaData = metaData;
            _serializer = serializer;
            _userDataSerializer = userDataSerializer;
        }

        public void ValidateNetworkData(NetworkData networkData)
        {
            if(networkData.Format != NetworkDataFormat.V1 && networkData.Format != NetworkDataFormat.V2)
                throw new InvalidOperationException("No cloud data source accepts only network config in version 1 or version 2.");

        }

        public void ValidateMetaData(IDictionary<string, string> metadata)
        {
            if (metadata.ContainsKey("instance-id"))
                throw new InvalidOperationException("metadata for instance-id is not allowed in custom metadata.");
            if (metadata.ContainsKey("local-hostname"))
                throw new InvalidOperationException("metadata for local-hostname is not allowed in custom metadata.");

        }

        public async Task<ConfigDriveContent> GenerateConfigDriveContent(
            IDictionary<string, string> metadata, NetworkData? networkData, IEnumerable<UserData> userData,
            GenerateConfigDriveOptions options)
        {
            var result = new ConfigDriveContent("cidata");


            ValidateMetaData(metadata);
            result.Files.Add(new ResultFile("meta-data",await SerializeMetaData(metadata)));

            if (networkData!= null)
            {
                ValidateNetworkData(networkData);
                result.Files.Add(new ResultFile("network-config", await _serializer.SerializeToYaml(networkData)));

            }

            var userDataArray = userData.ToArray();
            if(userDataArray.Length == 0)
                return result;

            result.Files.Add(new ResultFile("user-data", await _userDataSerializer.SerializeUserData(userDataArray, options.UserData)));

            return result;
        }


        private async Task<Stream> SerializeMetaData(IDictionary<string, string> metadata)
        {
            var localMetaData = new Dictionary<string, string>(metadata)
            {
                { "instance-id", _metaData.InstanceId },
                { "local-hostname", _metaData.HostName }
            };

            return await _serializer.SerializeToYaml(localMetaData);

        }

    }
}