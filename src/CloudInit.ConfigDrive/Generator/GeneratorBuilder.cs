using SimpleInjector;

namespace Contiva.CloudInit.ConfigDrive.Generator
{

    public class GeneratorBuilder : BaseBuilder
    {

        public static GeneratorBuilder Init()
        {
            var container = new Container();
            return new GeneratorBuilder(container);
        }

        protected GeneratorBuilder(Container container) : base(container)
        {
        }

        protected GeneratorBuilder(BaseBuilder innerBuilder): base(innerBuilder)
        {
        }
    }
}
