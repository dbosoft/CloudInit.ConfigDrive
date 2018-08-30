using System;
using Contiva.CloudInit.ConfigDrive.Generator;

namespace Contiva.CloudInit.ConfigDrive.Processing
{
    public class WindowsImagingProcessorBuilder : ProcessorBuilder
    {

        internal WindowsImagingProcessorBuilder(IBuilder innerBuilder) : base(innerBuilder)
        {
        }

        public ProcessorBuilder ImageFile(string filename)
        {
            Container
                .RegisterDecorator<ICommandHandler<ProcessResultCommand>, ImageFileProcessResultCommandHandlerDecoration>();
            Container.RegisterInitializer<ImageFileProcessResultCommandHandlerDecoration>(
                s => s.Filename = filename);

            return this;
        }


    }
}