
using Contiva.CloudInit.ConfigDrive.Injection;

namespace Contiva.CloudInit.ConfigDrive.Generator
{

    public class GeneratorBuilder : BaseBuilder
    {

        public static GeneratorBuilder Init()
        {
            var container = new Injectionist();
            return new GeneratorBuilder(container);
        }

        protected GeneratorBuilder(Injectionist container) : base(container)
        {
        }

        protected GeneratorBuilder(BaseBuilder innerBuilder): base(innerBuilder)
        {
        }
    }
}
