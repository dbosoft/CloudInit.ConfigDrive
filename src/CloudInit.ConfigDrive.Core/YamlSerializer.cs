using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Dbosoft.CloudInit.ConfigDrive
{
    public class YamlSerializer : IYamlSerializer
    {
        public Task<Stream> SerializeToYaml<T>(T data)
        {
            var serializer = new SerializerBuilder()
                .WithTypeConverter(new NetworkDataConverter())
                .Build();
            
            var sb = new StringBuilder();
            using (var writer = new StringWriter(sb))
                serializer.Serialize(writer, data ?? throw new ArgumentNullException(nameof(data)));


            return Task.FromResult( (Stream) new MemoryStream(Encoding.UTF8.GetBytes(sb.ToString())));
        }
    }

    internal class NetworkDataConverter : IYamlTypeConverter
    {
        public bool Accepts(Type type)
        {
            return typeof(NetworkData) == type;
        }

        public object? ReadYaml(IParser parser, Type type)
        {
            throw new NotImplementedException();
        }

        public void WriteYaml(IEmitter emitter, object? value, Type type)
        {
            var networkData = value as NetworkData;

            if (networkData == null)
                return;

            //var envelope = new NetworkDataEnvelope();
            NetworkWithVersion? output = null;

            switch (networkData.Format)
            {
                case NetworkDataFormat.V1:
                    output = new NetworkDataV1
                    {
                        Version = 1,
                        Config = networkData.Config as IList<object>
                    };
                    break;
                case NetworkDataFormat.V2:
                    var v2 = new NetworkDataV2
                    {
                        Version = 2
                    };
                    output = v2;

                    if (networkData.Config is IDictionary<string, object> dict)
                    {

                        dict.TryGetValue("ethernets", out var ethernets);
                        v2.Ethernets = ethernets;

                        dict.TryGetValue("bonds", out var bonds);
                        v2.Bonds = bonds;

                        dict.TryGetValue("bridges", out var bridges);
                        v2.Bridges = bridges;

                        dict.TryGetValue("vlans", out var vlans);
                        v2.VLANs = vlans;

                        dict.TryGetValue("routes", out var routes);
                        v2.Routes = routes;
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (output == null) return;
            var serializer = new SerializerBuilder().BuildValueSerializer();
            serializer.SerializeValue(emitter, output, output.GetType());
        }

        private class NetworkWithVersion
        {

            [YamlMember(Alias = "version", Order = 0)]
            public int Version { get; set; }

        }

        private class NetworkDataV1 : NetworkWithVersion
        {

            [YamlMember(Alias = "config", Order = 1)]
            public IList<object>? Config { get; set; }


        }

        private class NetworkDataV2 : NetworkWithVersion
        {

            [YamlMember(Alias = "ethernets", DefaultValuesHandling = DefaultValuesHandling.OmitNull, Order = 1)]
            public object? Ethernets { get; set; }
            

            [YamlMember(Alias = "bonds", DefaultValuesHandling = DefaultValuesHandling.OmitNull, Order = 2)]
            public object? Bonds { get; set; }

            [YamlMember(Alias = "bridges", DefaultValuesHandling = DefaultValuesHandling.OmitNull, Order = 3)]
            public object? Bridges { get; set; }

            [YamlMember(Alias = "vlans", DefaultValuesHandling = DefaultValuesHandling.OmitNull, Order = 4)]
            public object? VLANs { get; set; }

            [YamlMember(Alias = "routes", DefaultValuesHandling = DefaultValuesHandling.OmitNull, Order = 5)]
            public object? Routes { get; set; }

        }
    }
}