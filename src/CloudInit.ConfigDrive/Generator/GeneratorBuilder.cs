using Dbosoft.CloudInit.ConfigDrive;
using Dbosoft.CloudInit.ConfigDrive.Generator;
using Dbosoft.CloudInit.ConfigDrive.Injection;
using Dbosoft.CloudInit.ConfigDrive.Processing;

namespace Dosoft.CloudInit.ConfigDrive.Generator
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

        private GeneratorBuilder(Injectionist container) : base(container)
        {
        }

        protected GeneratorBuilder(IBuilder innerBuilder): base(innerBuilder)
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
