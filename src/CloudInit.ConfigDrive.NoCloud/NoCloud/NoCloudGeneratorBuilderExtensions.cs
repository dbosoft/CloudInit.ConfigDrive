using Contiva.CloudInit.ConfigDrive.Generator;

namespace Contiva.CloudInit.ConfigDrive.NoCloud
{
    public static class NoCloudGeneratorBuilderExtensions
    {
        public static NoCloudGeneratorBuilder NoCloud(this GeneratorBuilder builder, NoCloudConfigDriveMetaData metaData)
        {
            return new NoCloudGeneratorBuilder(builder) { Metadata = metaData }; ;
        }
    }
}