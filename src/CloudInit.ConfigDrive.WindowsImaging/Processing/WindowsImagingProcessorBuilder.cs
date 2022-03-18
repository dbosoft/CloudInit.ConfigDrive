namespace Dbosoft.CloudInit.ConfigDrive.Processing
{
    public class WindowsImagingProcessorBuilder : ProcessorBuilder
    {

        internal WindowsImagingProcessorBuilder(IBuilder innerBuilder) : base(innerBuilder)
        {
        }

        public ProcessorBuilder ImageFile(string filename)
        {
            Container
                .Decorate<ICommandHandler<ProcessResultCommand>>(
                    c=> new ImageFileProcessResultCommandHandlerDecoration(c.Get<ICommandHandler<ProcessResultCommand>>()){Filename = filename});

            return this;
        }


    }
}