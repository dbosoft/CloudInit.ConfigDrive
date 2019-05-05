using Haipa.CloudInit.ConfigDrive.Injection;
using Haipa.CloudInit.ConfigDrive.Processing;

namespace Haipa.CloudInit.ConfigDrive.Generator
{

    public class GeneratorBuilder : GenerateableBuilder
    {

        public static GeneratorBuilder Init()
        {
            var container = new Injectionist();
            container.Register<IConfigDriveGenerator>(
                c=> new ConfigDriveGenerator(
                    c.Get<ICommandHandler<ProcessResultCommand>>(),
                    c.Get<ICommandHandler<GenerateResultCommand>>()));

            return new GeneratorBuilder(container);
        }

        protected GeneratorBuilder(Injectionist container) : base(container)
        {
        }

        protected GeneratorBuilder(BaseBuilder innerBuilder): base(innerBuilder)
        {
        }

        protected override void PrepareBuild()
        {
            if(!Container.Has<ICommandHandler<ProcessResultCommand>>())
                Container.Register<ICommandHandler<ProcessResultCommand>>(
                    c=> new DummyCommandHandler<ProcessResultCommand>());

            base.PrepareBuild();
        }
    }
}
