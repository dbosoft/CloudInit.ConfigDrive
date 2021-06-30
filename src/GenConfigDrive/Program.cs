using System;
using System.IO;
using System.Text;
using CommandLine;
using Dbosoft.CloudInit.ConfigDrive.NoCloud;
using Dbosoft.CloudInit.ConfigDrive.Processing;
using Dosoft.CloudInit.ConfigDrive.Generator;
using Dosoft.CloudInit.ConfigDrive.Processing;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace Dbosoft.CloudInit.ConfigDrive
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            return Parser.Default.ParseArguments<NoCloudOptions>(args)
                .MapResult(
                    RunNoCloudAndReturnExitCode,
                    errs => 1);
        }

        private static int RunNoCloudAndReturnExitCode(NoCloudOptions opts)
        {
            try
            {
                var noCloudBuilder = GeneratorBuilder.Init()
                    .NoCloud(new NoCloudConfigDriveMetaData(opts.Hostname));

                if (!opts.NoSwapFile)
                    noCloudBuilder.SwapFile();

                if (!string.IsNullOrWhiteSpace(opts.Proxy))
                    noCloudBuilder.ProxySettings(new ConfigDriveProxySettings(opts.Proxy) {NoProxy = opts.NoProxy});

                if (!string.IsNullOrWhiteSpace(opts.UserData))
                    noCloudBuilder.UserData(ReadJsonFile(opts.UserData));

                if (!string.IsNullOrWhiteSpace(opts.NetworkData))
                    noCloudBuilder.NetworkData(ReadJsonFile(opts.NetworkData));


                if (!string.IsNullOrWhiteSpace(opts.Content))
                    noCloudBuilder.Content(opts.Content);

                var processingBuilder = noCloudBuilder.Processing();


                if (!string.IsNullOrWhiteSpace(opts.ImagePath))
                    processingBuilder.Image().ImageFile(opts.ImagePath);
                else
                {
                    processingBuilder.Callback(result =>
                    {
                        Console.WriteLine(result.MediaName);
                        foreach (var resultFile in result.Files)
                        {
                            using (var reader = new StreamReader(resultFile.Content, Encoding.UTF8))
                            {
                                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                                Console.WriteLine(reader.ReadToEnd());
                            }
                        }
                    });
                }
                
                processingBuilder.Generate();
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        private static JObject ReadJsonFile(string filename)
        {
            return JObject.Parse(File.ReadAllText(filename));
        }
    }

    [Verb("NoCloud", HelpText = "Generate a NoCloud cloud-init disk")]
    [UsedImplicitly(ImplicitUseTargetFlags.Members)]
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
