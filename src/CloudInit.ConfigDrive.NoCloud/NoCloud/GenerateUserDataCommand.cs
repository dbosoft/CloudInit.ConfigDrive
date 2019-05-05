using Newtonsoft.Json.Linq;

namespace Haipa.CloudInit.ConfigDrive.NoCloud
{
    internal class GenerateUserDataCommand
    {
        public JObject UserDataJson { get; set; }
    }
}