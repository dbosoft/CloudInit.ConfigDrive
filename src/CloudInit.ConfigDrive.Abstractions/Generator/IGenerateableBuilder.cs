namespace Haipa.CloudInit.ConfigDrive.Generator
{
    public interface IGenerateableBuilder : IBuilder
    {
        void Generate();
    }
}