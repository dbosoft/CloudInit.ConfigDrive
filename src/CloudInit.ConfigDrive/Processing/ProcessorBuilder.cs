using System;
using Contiva.CloudInit.ConfigDrive.Generator;

namespace Contiva.CloudInit.ConfigDrive.Processing
{
    public class ProcessorBuilder : GenerateableBuilder
    {

        internal ProcessorBuilder(IBuilder innerBuilder) : base(innerBuilder)
        {
        }

        public ProcessorBuilder Callback(Action<ConfigDriveContent> contentAction)
        {
            Container
                .RegisterDecorator<ICommandHandler<ProcessResultCommand>, CallbackProcessResultCommandHandlerDecoration>();
            Container.RegisterInitializer<CallbackProcessResultCommandHandlerDecoration>(
                s => s.ContentAction = contentAction);
            return this;
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