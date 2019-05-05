using Newtonsoft.Json.Linq;

namespace Haipa.CloudInit.ConfigDrive.NoCloud
{
    internal class GenerateNetworkDataCommandHandler : ICommandHandler<GenerateNetworkDataCommand>
    {
        public void HandleCommand(GenerateNetworkDataCommand command)
        {
            if (NetworkData == null)
                return;

            command.NetworkDataJson = NetworkData;
        }


        public JObject NetworkData { get; set; }


    }
}