using System;
using Haipa.CloudInit.ConfigDrive.Generator;

namespace Haipa.CloudInit.ConfigDrive.Processing
{
    public class ProcessorBuilder : GenerateableBuilder
    {
        public ProcessorBuilder(IBuilder innerBuilder) : base(innerBuilder)
        {
        }

        public ProcessorBuilder Callback(Action<ConfigDriveContent> contentAction)
        {
            Container
                .Decorate<ICommandHandler<ProcessResultCommand>>(
                c=> new CallbackProcessResultCommandHandlerDecoration(
                    c.Get<ICommandHandler<ProcessResultCommand>>()) {ContentAction = contentAction});
            return this;
        }


    }
}