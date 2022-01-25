using Newtonsoft.Json.Linq;

namespace Dbosoft.CloudInit.ConfigDrive.NoCloud
{
    internal class GenerateSwapConfigCommandHandlerDecorator : ICommandHandler<GenerateUserDataCommand>
    {
        private readonly ICommandHandler<GenerateUserDataCommand> _decoratedHandler;

        public GenerateSwapConfigCommandHandlerDecorator(ICommandHandler<GenerateUserDataCommand> decoratedHandler)
        {
            _decoratedHandler = decoratedHandler;
        }

        public string Filename { get; set; }
        public string Size { get; set; }

        public void HandleCommand(GenerateUserDataCommand command)
        {
            var userData = new JObject
            {
                ["swap"] = new JArray{
                    new JObject {
                        ["filename"] = Filename,
                        ["size"] = Size
                    }}
            };

            _decoratedHandler.HandleCommand(command);


            if (command.UserDataJson != null)
            {
                userData.Merge(command.UserDataJson, new JsonMergeSettings { MergeArrayHandling = MergeArrayHandling.Union });
            }

            command.UserDataJson = userData;
        }



    }
}