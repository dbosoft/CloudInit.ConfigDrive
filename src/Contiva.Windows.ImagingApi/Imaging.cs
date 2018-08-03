using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Contiva.Windows.ImagingApi.Interop;
using STATSTG = System.Runtime.InteropServices.ComTypes.STATSTG;

namespace Contiva.Windows.ImagingApi
{
    [ClassInterface(ClassInterfaceType.None)]
    public class Imaging
    {
        /// <summary>
        /// Burns data files to disc in a single session using files from a 
        /// single directory tree.
        /// </summary>
        /// <param name="recorder">Burning Device. Must be initialized.</param>
        /// <param name="path">Directory of files to burn.</param>
        public void BurnDirectory(String path)
        {
            // Define the new disc format and set the recorder

            // Create an image stream for a specified directory.

            // Create a new file system image and retrieve the root directory
            IFileSystemImage fsi = new MsftFileSystemImage();
            fsi.ChooseImageDefaultsForMediaType(IMAPI_MEDIA_PHYSICAL_TYPE.IMAPI_MEDIA_TYPE_DISK);
            fsi.FileSystemsToCreate =
                FsiFileSystems.FsiFileSystemJoliet | FsiFileSystems.FsiFileSystemISO9660;
            // Use legacy ISO 9660 Format

            // Add the directory to the disc file system
            IFsiDirectoryItem dir = fsi.Root;
            dir.AddTree(path, false);
            
            // Create an image from the file system
            Console.WriteLine("Writing content to disc...");
            IFileSystemImageResult result = fsi.CreateResultImage();

            // Data stream sent to the burning device
            IStream stream = result.ImageStream;
            using (var fileStream = File.OpenWrite("test.iso"))
            {
                using (var comStream = new AStream(stream))
                {
                    comStream.CopyTo(fileStream);
                }
                
            }

            Console.WriteLine("----- Finished writing content -----");
        }

    }


}
