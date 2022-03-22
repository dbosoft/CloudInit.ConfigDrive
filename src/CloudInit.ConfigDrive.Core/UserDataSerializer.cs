using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;

namespace Dbosoft.CloudInit.ConfigDrive
{
    public class UserDataSerializer : IUserDataSerializer
    {
        public async Task<Stream> SerializeUserData(IEnumerable<UserData> userData, UserDataOptions options)
        {
            var sb = new StringBuilder();
            sb.AppendLine("From nobody Fri Jan  11 07:00:00 1980");
            sb.AppendLine("Content-Type: multipart/mixed; boundary=\"==BOUNDARY==\"");
            sb.AppendLine("MIME-Version: 1.0");

            foreach (var data in userData)
            {
                sb.AppendLine("--==BOUNDARY==");
                sb.AppendLine("MIME-Version: 1.0");


                sb.AppendLine($"Content-Type: {data.ContentType.Name}; charset=\"{data.Encoding.BodyName}\"");

                var contentString = data.Content;
                if (options.Base64Encode)
                {
                    contentString = Convert.ToBase64String(data.Encoding.GetBytes(contentString), Base64FormattingOptions.InsertLineBreaks);
                    sb.AppendLine("Content-Transfer-Encoding: base64");
                }

                sb.AppendLine(contentString);


            }

            if (!options.GZip)
                return new MemoryStream(Encoding.ASCII.GetBytes(sb.ToString()));

            await using var dataStream = new MemoryStream(Encoding.ASCII.GetBytes(sb.ToString()));

            var compressedStream = new MemoryStream();
            await using var compressor = new GZipStream(compressedStream, CompressionMode.Compress, true);
            await dataStream.CopyToAsync(compressor);
            compressor.Close();
            compressedStream.Seek(0, SeekOrigin.Begin);
            return compressedStream;

        }
    }
}