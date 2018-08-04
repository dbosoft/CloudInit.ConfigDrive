using Newtonsoft.Json.Linq;

namespace Contiva.CloudInit.ConfigDrive.NoCloud
{
    internal class GenerateUserDataCommand
    {
        public JObject UserDataJson { get; set; }
    }
}