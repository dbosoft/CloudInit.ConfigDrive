using System;
using Dbosoft.CloudInit.ConfigDrive.Generator;

namespace Dbosoft.CloudInit.ConfigDrive.Processing
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