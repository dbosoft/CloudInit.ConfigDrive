using Haipa.CloudInit.ConfigDrive.Injection;

namespace Haipa.CloudInit.ConfigDrive.Generator
{
    public class GenerateableBuilder : BaseBuilder, IGenerateableBuilder
    {
        protected GenerateableBuilder(Injectionist container) : base(container)
        {
            
        }

        protected GenerateableBuilder(IBuilder innerBuilder) : base(innerBuilder)
        {
        }

        public void Generate()
        {
            var generator = Build();
            generator.Generate();
        }

    }
}