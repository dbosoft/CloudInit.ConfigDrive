using System.Dynamic;
using System.IO;
using System.Text;
using Dbosoft.CloudInit.ConfigDrive.Generator;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using YamlDotNet.Serialization;

namespace Dbosoft.CloudInit.ConfigDrive.NoCloud
{
    internal class NoCloudGenerateResultCommandHandler : ICommandHandler<GenerateResultCommand>
    {
        private readonly ICommandHandler<GenerateMetaDataCommand> _metaDataCommandHandler;
        private readonly ICommandHandler<GenerateUserDataCommand> _userDataCommandHandler;
        private readonly ICommandHandler<GenerateNetworkDataCommand> _networkDataCommandHandler;

        public NoCloudGenerateResultCommandHandler(ICommandHandler<GenerateMetaDataCommand> metaDataCommandHandler, ICommandHandler<GenerateUserDataCommand> userDataCommandHandler, ICommandHandler<GenerateNetworkDataCommand> networkDataCommandHandler)
        {
            _metaDataCommandHandler = metaDataCommandHandler;
            _userDataCommandHandler = userDataCommandHandler;
            _networkDataCommandHandler = networkDataCommandHandler;
        }

        public void HandleCommand(GenerateResultCommand command)
        {
            command.Content = new ConfigDriveContent {MediaName = "cidata"};

            var metadataCommand = new GenerateMetaDataCommand();
            _metaDataCommandHandler.HandleCommand(metadataCommand);

            var userDataCommand = new GenerateUserDataCommand();
            _userDataCommandHandler.HandleCommand(userDataCommand);

            var networkDataCommand = new GenerateNetworkDataCommand();
            _networkDataCommandHandler.HandleCommand(networkDataCommand);

            if (metadataCommand.MetaDataJson != null)
            {
                command.Content.Files.Add(JsonToResultFile("meta-data",metadataCommand.MetaDataJson));
            }

            if (userDataCommand.UserDataJson != null)
            {
                command.Content.Files.Add(JsonToResultFile("user-data", userDataCommand.UserDataJson, "#cloud-config"));
            }

            if (networkDataCommand.NetworkDataJson != null)
            {
                command.Content.Files.Add(JsonToResultFile("network-config", networkDataCommand.NetworkDataJson));
            }
        }

        private static ResultFile JsonToResultFile(string path, JObject json, string header = null)
        {
            var content = header;

            if (content != null)
                content += "\n" + JsonToYaml(json).Replace("00:15:5d:ac:ac:9d", "\"0:15:5d:ac:ac:9d\"");
            else
                content = JsonToYaml(json);
            
            return new ResultFile
            {
                Path = path,
                Content = StringToStream(content)
            };
        }

        private static Stream StringToStream(string data)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(data.Replace("\r\n", "\n")));
        }

        private static string JsonToYaml(JObject json)
        {
            var jsonString = JsonConvert.SerializeObject(json);
            var expConverter = new ExpandoObjectConverter();
            dynamic deserializedObject = JsonConvert.DeserializeObject<ExpandoObject>(jsonString, expConverter);

            var serializer = new SerializerBuilder().DisableAliases().Build();
            string yaml = serializer.Serialize(deserializedObject);

            return yaml;
        }
    }
}