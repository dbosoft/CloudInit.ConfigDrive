using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using JetBrains.Annotations;

namespace Dbosoft.CloudInit.ConfigDrive
{
    [ExcludeFromCodeCoverage]
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var result = await Parser.Default
                .ParseArguments<NoCloudOptions>(args)
                .WithParsedAsync(RunNoCloudAndReturnExitCode);

           
            //return Parser.Default.ParseArguments<NoCloudOptions>(args)

        }

        private static async Task<int> RunNoCloudAndReturnExitCode(NoCloudOptions opts)
        {
            try
            {
                var configDrive = new ConfigDriveBuilder()
                    .NoCloud(new NoCloudConfigDriveMetaData(opts.Hostname))
                    .Build();

                if (!string.IsNullOrWhiteSpace(opts.UserData))
                    configDrive.AddUserData(
                        new UserData(UserDataContentType.CloudConfig, await File.ReadAllTextAsync(opts.UserData), Encoding.UTF8));

                //if (!string.IsNullOrWhiteSpace(opts.NetworkData))
                //    noCloudBuilder.NetworkData(ReadJsonFile(opts.NetworkData));

                var outputBuilder = new StringBuilder();
                IConfigDriveWriter writer = new ConfigDriveStringWriter(outputBuilder);
                if (!string.IsNullOrWhiteSpace(opts.ImagePath))
                {
                    writer = new ConfigDriveImageWriter(opts.ImagePath);
                }

                await writer.WriteConfigDrive(configDrive);
                Console.WriteLine(outputBuilder.ToString());
                
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

    }

    [Verb("NoCloud", HelpText = "Generate a NoCloud cloud-init disk")]
    [UsedImplicitly(ImplicitUseTargetFlags.Members)]
    [ExcludeFromCodeCoverage]
    internal class NoCloudOptions
    {
        [Option(HelpText = "Path to cloud-init user data")]
        public string UserData { get; set; }

        [Option(HelpText = "Path to cloud-init network data")]
        public string NetworkData { get; set; }

        [Option(HelpText = "Path to content file")]
        public string Content { get; set; }


        [Option(Required = true)]
        public string Hostname { get; set; }

        [Option]
        public string InstanceId { get; set; }


        [Option("no-swapfile")]
        public bool NoSwapFile { get; set; }

        [Option("proxy")]
        public string Proxy { get; set; }

        [Option("no-proxy")]
        public string NoProxy { get; set; }


        [Option]
        public string ImagePath { get; set; }

    }

}
