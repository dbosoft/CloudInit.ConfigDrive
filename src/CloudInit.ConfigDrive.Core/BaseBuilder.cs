using Dbosoft.CloudInit.ConfigDrive.Injection;

namespace Dbosoft.CloudInit.ConfigDrive
{
    public class BaseBuilder: IBuilder
    {
        private readonly Injectionist _container;
        private readonly BaseBuilder _innerBuilder;

        protected BaseBuilder(Injectionist container)
        {
            _container = container;
        }

        protected BaseBuilder(IBuilder innerBuilder)
        {
            _innerBuilder = innerBuilder as BaseBuilder;
        }

        protected Injectionist Container => _container ?? _innerBuilder.Container;


        public virtual BaseBuilder With<T>(T instance) where T : class
        {
            Container.Register(c=> instance);
            return this;
        }

        protected IConfigDriveGenerator Build()
        {
            PrepareBuild();

            if (!Container.Has<IConfigDriveGenerator>())
                throw new CloudInitConfigurationException("No Config Drive Generator has been configured");

            return Container.Get<IConfigDriveGenerator>().Instance;

        }

        protected virtual void PrepareBuild()
        {
            _innerBuilder?.PrepareBuild();
        }
    }

}