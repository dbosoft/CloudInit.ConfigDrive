using System;

namespace Dbosoft.CloudInit.ConfigDrive.Processing
{
    internal class CallbackProcessResultCommandHandlerDecoration : ICommandHandler<ProcessResultCommand>
    {
        public Action<ConfigDriveContent> ContentAction { get; set; }
        private readonly ICommandHandler<ProcessResultCommand> _decoratedHandler;

        public CallbackProcessResultCommandHandlerDecoration(ICommandHandler<ProcessResultCommand> decoratedHandler)
        {
            _decoratedHandler = decoratedHandler;
        }

        public void HandleCommand(ProcessResultCommand command)
        {
            ContentAction?.Invoke(command.Content);
            _decoratedHandler.HandleCommand(command);
        }
    }
}
