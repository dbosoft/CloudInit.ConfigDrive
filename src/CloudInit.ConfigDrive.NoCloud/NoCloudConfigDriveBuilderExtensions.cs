
namespace Dbosoft.CloudInit.ConfigDrive
{
    public static class NoCloudConfigDriveBuilderExtensions
    {
        public static IConfigDriveBuilder NoCloud(this IConfigDriveBuilder builder, NoCloudConfigDriveMetaData metaData)
        {
            builder.With<IConfigDriveDataSource>(ctx => 
                new NoCloudDataSource(metaData, 
                    ctx.Get<IYamlSerializer>(), 
                    ctx.Get<IUserDataSerializer>()));

            return builder;
        }
    }
}