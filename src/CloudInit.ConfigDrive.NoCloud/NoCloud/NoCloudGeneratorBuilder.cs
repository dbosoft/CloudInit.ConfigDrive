using Contiva.CloudInit.ConfigDrive.Generator;
using Contiva.CloudInit.ConfigDrive.Processing;
using Newtonsoft.Json.Linq;

namespace Contiva.CloudInit.ConfigDrive.NoCloud
{
    public class NoCloudGeneratorBuilder : GenerateableBuilder, IProcessableBuilder
    {
        internal NoCloudGeneratorBuilder(IBuilder innerBuilder) : base(innerBuilder)
        {
            Container.Register<ICommandHandler<GenerateResultCommand>, NoCloudGenerateResultCommandHandler>();

            Container.Register<ICommandHandler<GenerateMetaDataCommand>, GenerateMetaDataCommandHandler>();
            Container.RegisterInitializer< GenerateMetaDataCommandHandler>(h => h.Metadata = Metadata);

            Container.Register<ICommandHandler<GenerateUserDataCommand>, GenerateUserDataCommandHandler>();
            Container.RegisterInitializer<GenerateUserDataCommandHandler>(h => h.UserData = _userData);

            Container.Register<ICommandHandler<GenerateNetworkDataCommand>, GenerateNetworkDataCommandHandler>();
            Container.RegisterInitializer<GenerateNetworkDataCommandHandler>(h => h.NetworkData = _networkData);

        }

        public NoCloudGeneratorBuilder SwapFile(string filename = "/swap.img", string size = "auto")
        {
            Container.RegisterDecorator<ICommandHandler<GenerateUserDataCommand>, GenerateSwapConfigCommandHandlerDecorator>();
            Container.RegisterInitializer<GenerateSwapConfigCommandHandlerDecorator>(h =>
            {
                h.Filename = filename;
                h.Size = size;
            });
            return this;
        }

        public NoCloudGeneratorBuilder Content(string filename)
        {
            Container.RegisterDecorator<ICommandHandler<GenerateUserDataCommand>, GenerateContentConfigCommandHandlerDecorator>();
            Container.RegisterInitializer<GenerateContentConfigCommandHandlerDecorator>(h =>
            {
                h.Filename = filename;

            });
            return this;
        }

        public NoCloudGeneratorBuilder ProxySettings(ConfigDriveProxySettings proxySettings)
        {
            Container.RegisterDecorator<ICommandHandler<GenerateUserDataCommand>, GenerateProxyConfigCommandHandlerDecorator>();
            Container.RegisterInitializer<GenerateProxyConfigCommandHandlerDecorator>(h =>
            {
                h.ProxySettings = proxySettings;
            });
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