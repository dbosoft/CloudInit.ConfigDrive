using SimpleInjector;

namespace Contiva.CloudInit.ConfigDrive.Generator
{
    public class BaseBuilder: IBuilder
    {
        private readonly Container _container;
        private readonly BaseBuilder _innerBuilder;

        protected BaseBuilder(Container container)
        {
            _container = container;
        }

        protected BaseBuilder(IBuilder innerBuilder)
        {
            _innerBuilder = innerBuilder as BaseBuilder;
        }

        protected Container Container => _container ?? _innerBuilder.Container;


        public virtual BaseBuilder With<T>(T instance) where T : class
        {
            Container.RegisterInstance(instance);
            return this;
        }

        protected IConfigDriveGenerator Build()
        {
            PrepareBuild();

            Container.RegisterConditional<IConfigDriveGenerator, ConfigDriveGenerator>(c => !c.Handled);
            Container.RegisterConditional(typeof(ICommandHandler<>), typeof(DummyCommandHandler<>), c => !c.Handled);

            Container.Verify();

            return Container.GetInstance<IConfigDriveGenerator>();

        }

       
        protected virtual void PrepareBuild()
        {
            _innerBuilder?.PrepareBuild();
        }
    }
}