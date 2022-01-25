using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Dbosoft.CloudInit.ConfigDrive.NoCloud
{
    internal class GenerateContentConfigCommandHandlerDecorator : ICommandHandler<GenerateUserDataCommand>
    {
        private readonly ICommandHandler<GenerateUserDataCommand> _decoratedHandler;

        public GenerateContentConfigCommandHandlerDecorator(ICommandHandler<GenerateUserDataCommand> decoratedHandler)
        {
            _decoratedHandler = decoratedHandler;
        }

        public string Filename { get; set; }

        public void HandleCommand(GenerateUserDataCommand command)
        {
            var content = Convert.ToBase64String(File.ReadAllBytes(Filename));

            var userData = new JObject
            {
                ["packages"] = new JArray
                {
                    "unzip"

                },

                ["write_files"] = new JArray
                {
                    new JObject
                    {
                        ["encoding"] = "b64",
                        ["content"] = content,
                        ["owner"] = "root:root",
                        ["path"] = "/tmp/init.zip",
                        ["permission"] = "0644"

                    }
                },

                ["runcmd"] = new JArray
                {
                    "unzip /tmp/init.zip -d /tmp/init",
                    "rm /tmp/init.zip",
                    "chmod +x /tmp/init/init.sh",
                    "/tmp/init/init.sh"

                }
            };


            _decoratedHandler.HandleCommand(command);


            if (command.UserDataJson != null)
            {
                userData.Merge(command.UserDataJson, new JsonMergeSettings{ MergeArrayHandling = MergeArrayHandling.Union});
            }

            command.UserDataJson = userData;
        }



    }
}