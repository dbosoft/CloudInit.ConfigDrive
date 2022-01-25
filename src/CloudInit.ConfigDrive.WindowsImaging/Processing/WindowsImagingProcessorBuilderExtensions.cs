namespace Dbosoft.CloudInit.ConfigDrive.Processing
{
    public static class WindowsImagingProcessorBuilderExtensions
    {
        public static WindowsImagingProcessorBuilder Image(this ProcessorBuilder builder)
        {
            return new WindowsImagingProcessorBuilder(builder);
        }
    }
}