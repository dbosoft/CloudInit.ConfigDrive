using Newtonsoft.Json.Linq;

namespace Dbosoft.CloudInit.ConfigDrive.NoCloud
{
    internal class GenerateUserDataCommand
    {
        public JObject UserDataJson { get; set; }
    }
}