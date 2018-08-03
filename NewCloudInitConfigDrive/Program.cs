using System;
using System.Collections.Generic;
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

            Imaging samples = new Imaging();

            samples.BurnDirectory(@"T:\chef\kitchen");

        }
    }
}
