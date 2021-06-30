using Newtonsoft.Json.Linq;

namespace Dbosoft.CloudInit.ConfigDrive.NoCloud
{
    internal class GenerateMetaDataCommandHandler : ICommandHandler<GenerateMetaDataCommand>
    {
        public void HandleCommand(GenerateMetaDataCommand command)
        {
            if (command.MetaDataJson != null)
                return;

            command.MetaDataJson = new JObject
            {
                ["instance-id"] = Metadata.InstanceId,
                ["local-hostname"] = Metadata.HostName
            };
        }

       
        public NoCloudConfigDriveMetaData Metadata { get; set; }


    }
}