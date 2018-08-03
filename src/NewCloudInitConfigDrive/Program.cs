using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contiva.Windows.ImagingApi;
using Contiva.Windows.ImagingApi.Interop;

namespace NewCloudInitConfigDrive
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a DiscMaster2 object to connect to CD/DVD drives.

            var builder = new IsoImageBuilder(FileSystemType.Udf);

            builder.AddDirectory(@"T:\chef\kitchen");
            using(var fileStream = File.OpenWrite("test.iso"))
            using (var imageStream = builder.Build())
            {
                imageStream.CopyTo(fileStream);
            }
        }
    }
}
