using System.Collections.Generic;
using System.Data;

namespace Dbosoft.CloudInit.ConfigDrive
{
    public class NetworkData
    {
        public object Config { get; }

        public readonly NetworkDataFormat Format;

        public NetworkData(IEnumerable<object> config)
        {
            Config = config;
            Format = NetworkDataFormat.V1;
        }

        public NetworkData(IDictionary<string, object> config)
        {
            Config = config;
            Format = NetworkDataFormat.V2;
        }
    }
}