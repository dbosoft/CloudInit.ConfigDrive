namespace Contiva.CloudInit.ConfigDrive.Generator
{
    public class GenerateableBuilder : BaseBuilder, IGenerateableBuilder
    {

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