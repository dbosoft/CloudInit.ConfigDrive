using Newtonsoft.Json.Linq;

namespace Contiva.CloudInit.ConfigDrive.NoCloud
{
    internal class GenerateMetaDataCommand
    {
        public JObject MetaDataJson { get; set; }
    }
}