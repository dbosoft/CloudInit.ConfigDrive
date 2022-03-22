using System;
using Dbosoft.CloudInit.ConfigDrive.Injection;

namespace Dbosoft.CloudInit.ConfigDrive
{
    public sealed class ConfigDriveBuilder : IConfigDriveBuilder
    {
        private readonly Injectionist _container;

        public ConfigDriveBuilder() : this(new Injectionist())
        {}


        public ConfigDriveBuilder(Injectionist container)
        {
            _container = container;
            _container.Register<IConfigDrive>( ctx => new ConfigDrive(
                ctx.Get<IConfigDriveDataSource>() ) );

            _container.Register<IYamlSerializer>(ctx => new YamlSerializer());
            _container.Register<IUserDataSerializer>(ctx => new UserDataSerializer());

        }

        public IConfigDriveBuilder With<T>(T instance) where T : class
        {
            _container.Register(c=> instance);
            return this;
        }

        public IConfigDriveBuilder With<T>(Func<IResolutionContext, T> resolverMethod) where T : class
        {
            _container.Register(resolverMethod);
            return this;
        }

        public IConfigDrive Build()
        {

            if (!_container.Has<IConfigDriveDataSource>())
                throw new CloudInitConfigurationException("No data source has been configured");

            return _container.Get<IConfigDrive>().Instance;

        }

    }
}