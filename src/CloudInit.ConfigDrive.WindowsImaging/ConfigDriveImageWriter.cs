using System;
using System.IO;
using System.Threading.Tasks;
using Dbosoft.CloudInit.ConfigDrive.Interop;

namespace Dbosoft.CloudInit.ConfigDrive
{
    public class ConfigDriveImageWriter : IConfigDriveWriter
    {
        private readonly string _fileName;
        private readonly FsiFileSystems _fileSystems;
        private readonly int _iso9660InterchangeLevel;

        public ConfigDriveImageWriter(string fileName, FileSystemType fileSystemType = FileSystemType.Joliet)
        {
            _fileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
            switch (fileSystemType)
            {
                case FileSystemType.Iso9660:
                    _fileSystems =
                        FsiFileSystems.FsiFileSystemISO9660;
                    _iso9660InterchangeLevel = 2;
                    break;
                case FileSystemType.Joliet:
                    _fileSystems =
                        FsiFileSystems.FsiFileSystemJoliet | FsiFileSystems.FsiFileSystemISO9660;
                    _iso9660InterchangeLevel = 2;

                    break;
                case FileSystemType.Udf:
                    _fileSystems =
                        FsiFileSystems.FsiFileSystemUDF;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fileSystemType), fileSystemType, null);
            }
        }

        public readonly UserDataOptions UserDataOptions = new();

        public async Task WriteConfigDrive(IConfigDrive configDrive)
        {
            var content = await configDrive.GenerateContent(new GenerateConfigDriveOptions
            {
                UserData =
                {
                    Base64Encode = UserDataOptions.Base64Encode,
                    GZip = UserDataOptions.GZip
                }
            });
            ;
            var fsi = new MsftFileSystemImage
            {
                VolumeName = content.MediaName,
                
            };
            
            fsi.ChooseImageDefaultsForMediaType(IMAPI_MEDIA_PHYSICAL_TYPE.IMAPI_MEDIA_TYPE_DISK);
            fsi.FileSystemsToCreate = _fileSystems;
            fsi.ISO9660InterchangeLevel = _iso9660InterchangeLevel;
            
            var dir = fsi.Root;

            foreach (var file in content.Files) 
            {
                dir.AddFile(file.Path, new ImageStream(file.Content));

            }

            var result = fsi.CreateResultImage();

            // Data stream sent to the burning device
            var stream = result.ImageStream;

            await using var isoStream = new ImageStream(stream);
            await using var fileStream = File.OpenWrite(_fileName);
            await isoStream.CopyToAsync(fileStream);
            fileStream.Close();

        }
    }
}
