using Newtonsoft.Json.Linq;

namespace Dbosoft.CloudInit.ConfigDrive.NoCloud
{
    internal class GenerateUserDataCommandHandler : ICommandHandler<GenerateUserDataCommand>
    {
        public void HandleCommand(GenerateUserDataCommand command)
        {
            if (UserData == null)
                return;

            command.UserDataJson = UserData;
        }


        public JObject UserData { get; set; }


    }
}