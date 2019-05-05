using Haipa.CloudInit.ConfigDrive.Processing;

namespace Haipa.CloudInit.ConfigDrive.Generator
{
    class ConfigDriveGenerator : IConfigDriveGenerator
    {
        private readonly ICommandHandler<ProcessResultCommand> _resultCommandHandler;
        private readonly ICommandHandler<GenerateResultCommand> _generateCommandHandler;

        public ConfigDriveGenerator(
            ICommandHandler<ProcessResultCommand> resultCommandHandler, 
            ICommandHandler<GenerateResultCommand> generateCommandHandler)
        {
            _resultCommandHandler = resultCommandHandler;
            _generateCommandHandler = generateCommandHandler;
        }

        public void Generate()
        {
            var generateCommand = new GenerateResultCommand();
            _generateCommandHandler.HandleCommand(generateCommand);

            _resultCommandHandler.HandleCommand(new ProcessResultCommand
            {
                Content = generateCommand.Content
            });
        }
    }
}