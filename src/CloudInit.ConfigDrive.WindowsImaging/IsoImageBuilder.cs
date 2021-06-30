using System;
using System.IO;
using System.Runtime.InteropServices;
using Dbosoft.CloudInit.ConfigDrive.Interop;

// ReSharper disable UnusedMember.Global

namespace Dbosoft.CloudInit.ConfigDrive
{
    [ClassInterface(ClassInterfaceType.None)]
    internal sealed class IsoImageBuilder
    {
        private readonly MsftFileSystemImage _fsi = new MsftFileSystemImage();

        public IsoImageBuilder(FileSystemType fileSystemType)
        {
            _fsi.ChooseImageDefaultsForMediaType(IMAPI_MEDIA_PHYSICAL_TYPE.IMAPI_MEDIA_TYPE_DISK);
            
            switch (fileSystemType)
            {
                case FileSystemType.Iso9660:
                    _fsi.FileSystemsToCreate =
                        FsiFileSystems.FsiFileSystemISO9660;
                    _fsi.ISO9660InterchangeLevel = 2;

                    break;
                case FileSystemType.Joliet:
                    _fsi.FileSystemsToCreate =
                        FsiFileSystems.FsiFileSystemJoliet | FsiFileSystems.FsiFileSystemISO9660;
                    _fsi.ISO9660InterchangeLevel = 2;
                    
                    break;
                case FileSystemType.Udf:
                    _fsi.FileSystemsToCreate =
                        FsiFileSystems.FsiFileSystemUDF;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fileSystemType), fileSystemType, null);
            }

        }



        public IsoImageBuilder AddDirectory(string path)
        {
            var dir = _fsi.Root;
            dir.AddTree(path, false);

            return this;
        }

        public IsoImageBuilder AddFile(string path, Stream data)
        {
            var dir = _fsi.Root;
            dir.AddFile(path, new ImageStream(data));

            return this;
        }

        public IsoImageBuilder VolumeName(string name)
        {
            _fsi.VolumeName = name;
            return this;
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
