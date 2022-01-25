using System.Text;
using Newtonsoft.Json.Linq;

namespace Dbosoft.CloudInit.ConfigDrive.NoCloud
{
    internal class GenerateProxyConfigCommandHandlerDecorator : ICommandHandler<GenerateUserDataCommand>
    {
        private readonly ICommandHandler<GenerateUserDataCommand> _decoratedHandler;

        public GenerateProxyConfigCommandHandlerDecorator(ICommandHandler<GenerateUserDataCommand> decoratedHandler)
        {
            _decoratedHandler = decoratedHandler;
        }

        public ConfigDriveProxySettings ProxySettings { get; set; }

        public void HandleCommand(GenerateUserDataCommand command)
        {

            var proxyEnvScriptBuilder = new StringBuilder();
            proxyEnvScriptBuilder.AppendLine(@"#!/bin/sh");
            proxyEnvScriptBuilder.AppendLine(@"cat >> /etc/environment << EOC");
            proxyEnvScriptBuilder.AppendLine($"http_proxy = \"{ProxySettings.HttpProxy}\"");
            proxyEnvScriptBuilder.AppendLine($"https_proxy = \"{ProxySettings.HttpsProxy}\"");
            proxyEnvScriptBuilder.AppendLine($"ftp_proxy = \"{ProxySettings.FtpProxy}\"");
            proxyEnvScriptBuilder.AppendLine($"no_proxy = \"{ProxySettings.NoProxy}\"");
            proxyEnvScriptBuilder.AppendLine("EOC");

            var proxyEnvScript = proxyEnvScriptBuilder.ToString();

            var userData = new JObject
            {
                ["apt"] = new JObject
                {
                    ["proxy"] = ProxySettings.HttpProxy,
                    ["http_proxy"] = ProxySettings.HttpProxy,
                    ["ftp_proxy"] = ProxySettings.FtpProxy,
                    ["https_proxy"] = ProxySettings.HttpsProxy
                },

                ["write_files"] = new JArray
                {
                    new JObject
                    {
                        ["content"] = proxyEnvScript,
                        ["path"] = "/tmp/proxysetup.sh"
                    }
                },

                ["runcmd"] = new JArray
                {
                    "chmod +x /tmp/proxysetup.sh",
                    "/tmp/proxysetup.sh",
                    "rm /tmp/proxysetup.sh"
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