using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Dbosoft.CloudInit.ConfigDrive
{
    public class ConfigDriveStringWriter : IConfigDriveWriter
    {
        private readonly StringBuilder _stringBuilder;

        public ConfigDriveStringWriter(StringBuilder stringBuilder)
        {
            _stringBuilder = stringBuilder;
        }

        public async Task WriteConfigDrive(IConfigDrive configDrive)
        {
            var content = await configDrive.GenerateContent(new GenerateConfigDriveOptions
            {
                UserData =
                {
                    Base64Encode = false,
                    GZip = false
                }
            });

            _stringBuilder.AppendLine($"Media name: {content.MediaName}");

            foreach (var file in content.Files)
            {
                _stringBuilder.AppendLine($"--- file: {file.Path} ---");
                using var streamReader = new StreamReader(file.Content);
                var fileContent = await streamReader.ReadToEndAsync();
                _stringBuilder.AppendLine(fileContent);
            }


        }
    }
}
