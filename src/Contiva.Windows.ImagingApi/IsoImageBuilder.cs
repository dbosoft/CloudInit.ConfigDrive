using System;
using System.IO;
using System.Runtime.InteropServices;
using Contiva.Windows.ImagingApi.Interop;
// ReSharper disable UnusedMember.Global

namespace Contiva.Windows.ImagingApi
{
    [ClassInterface(ClassInterfaceType.None)]
    public sealed class IsoImageBuilder
    {
        private readonly IFileSystemImage _fsi = new MsftFileSystemImage();

        public IsoImageBuilder(FileSystemType fileSystemType)
        {

            _fsi.ChooseImageDefaultsForMediaType(IMAPI_MEDIA_PHYSICAL_TYPE.IMAPI_MEDIA_TYPE_DISK);

            switch (fileSystemType)
            {
                case FileSystemType.Iso9660:
                    _fsi.FileSystemsToCreate =
                        FsiFileSystems.FsiFileSystemISO9660;

                    break;
                case FileSystemType.Joliet:
                    _fsi.FileSystemsToCreate =
                        FsiFileSystems.FsiFileSystemJoliet | FsiFileSystems.FsiFileSystemISO9660;

                    break;
                case FileSystemType.Udf:
                    _fsi.FileSystemsToCreate =
                        FsiFileSystems.FsiFileSystemUDF;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fileSystemType), fileSystemType, null);
            }

        }



        public void AddDirectory(string path)
        {
            var dir = _fsi.Root;
            dir.AddTree(path, false);

        }

        public void AddFile(string path, Stream data)
        {
            var dir = _fsi.Root;
            dir.AddFile(path, new ImageStream(data));

        }

        public Stream Build()
        {

            var result = _fsi.CreateResultImage();

            // Data stream sent to the burning device
            var stream = result.ImageStream;
            return new ImageStream(stream);
        }

    }


    public enum FileSystemType
    {
        Iso9660 = 1,
        Joliet = 2,
        Udf = 4,
    }

}
