using Dbosoft.CloudInit.ConfigDrive.Generator;
using Dbosoft.CloudInit.ConfigDrive.Processing;
using Newtonsoft.Json.Linq;

namespace Dbosoft.CloudInit.ConfigDrive.NoCloud
{
    public class NoCloudGeneratorBuilder : GenerateableBuilder, IProcessableBuilder
    {
        internal NoCloudGeneratorBuilder(IBuilder innerBuilder) : base(innerBuilder)
        {
            Container.Register<ICommandHandler<GenerateResultCommand>>(
                c=> new NoCloudGenerateResultCommandHandler(
                    c.Get<ICommandHandler<GenerateMetaDataCommand>>(),
                    c.Get<ICommandHandler<GenerateUserDataCommand>>(),
                    c.Get<ICommandHandler<GenerateNetworkDataCommand>>()));

            Container.Register<ICommandHandler<GenerateMetaDataCommand>>(
                c=> new GenerateMetaDataCommandHandler{ Metadata = Metadata});

            Container.Register<ICommandHandler<GenerateUserDataCommand>>(
                c => new GenerateUserDataCommandHandler { UserData = _userData });

            Container.Register<ICommandHandler<GenerateNetworkDataCommand>>(
                c => new GenerateNetworkDataCommandHandler { NetworkData = _networkData });

        }

        public NoCloudGeneratorBuilder SwapFile(string filename = "/swap.img", string size = "auto")
        {
            Container.Decorate<ICommandHandler<GenerateUserDataCommand>>(
                c => new GenerateSwapConfigCommandHandlerDecorator(
                    c.Get<ICommandHandler<GenerateUserDataCommand>>()){ Filename = filename, Size = size});

            return this;
        }

        public NoCloudGeneratorBuilder Content(string filename)
        {
            Container.Decorate<ICommandHandler<GenerateUserDataCommand>>(c =>
                new GenerateContentConfigCommandHandlerDecorator(
                    c.Get<ICommandHandler<GenerateUserDataCommand>>()) {Filename = filename});

            return this;
        }

        public NoCloudGeneratorBuilder ProxySettings(ConfigDriveProxySettings proxySettings)
        {
            Container.Decorate<ICommandHandler<GenerateUserDataCommand>>(
                c=> new GenerateProxyConfigCommandHandlerDecorator(c.Get<ICommandHandler<GenerateUserDataCommand>>()){ProxySettings = proxySettings});

            return this;
        }

        public NoCloudGeneratorBuilder UserData(JObject userdata)
        {
            _userData = userdata;
            return this;
        }

        public NoCloudGeneratorBuilder NetworkData(JObject networkData)
        {
            _networkData = networkData;
            return this;
        }

        public NoCloudConfigDriveMetaData Metadata { get; set; }

        private JObject _userData;
        private JObject _networkData;
    }
}