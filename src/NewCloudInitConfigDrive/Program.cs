using System;
using System.IO;
using System.Text;
using Contiva.CloudInit.ConfigDrive;
using Contiva.CloudInit.ConfigDrive.Generator;
using Contiva.CloudInit.ConfigDrive.NoCloud;
using Contiva.CloudInit.ConfigDrive.Processing;
using Newtonsoft.Json.Linq;

namespace NewCloudInitConfigDrive
{
    class Program
    {
        static void Main(string[] args)
        {

            GeneratorBuilder.Init()
                .NoCloud(new NoCloudConfigDriveMetaData("cool2"))
                    .Content("T:\\openstack\\osbootstrap-init.zip")
//                    .NetworkData(JObject.Parse(@"{
//    'version' : '1',
//    'config' :[ { 
//        'type': 'physical',
//        'name' : 'eth0',
//        'mac_address' : '00:15:5d:ac:ac:9d',
//        'subnets' : [ {
//		    'type' : 'static',
//		    'address' : '192.168.3.140/24',
//		    'gateway' : '192.168.3.1',
//		    'dns_nameservers':  [ '192.168.2.6', '192.168.2.9' ]
//			}
//            ]
//        }
//]
//} 
    
//"))
                    .UserData(JObject.Parse(
                        @"{
'password': 'password1', 
'chpasswd': { 
    'expire': false }, 
'system_info' : {
  'default_user': {
    	'name': 'world'
  } }
        }"))
                    .SwapFile()
                    .ProxySettings(new ConfigDriveProxySettings())
                .Processing()
                    .Callback(r =>
                    {
                        foreach (var resultFile in r.Files)
                        {
                            using (var reader = new StreamReader(resultFile.Content, Encoding.UTF8))
                            {
                                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                                Console.WriteLine(reader.ReadToEnd());
                            }
                        }
                        Console.WriteLine(r.MediaName);
                    })
                    .ImageFile("test.iso")
                .Generate();


        }

    }
}
