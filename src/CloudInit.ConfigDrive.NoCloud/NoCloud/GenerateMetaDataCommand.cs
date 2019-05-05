using Newtonsoft.Json.Linq;

namespace Haipa.CloudInit.ConfigDrive.NoCloud
{
    internal class GenerateMetaDataCommand
    {
        public JObject MetaDataJson { get; set; }
    }
}