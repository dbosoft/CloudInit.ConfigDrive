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
            
            sb.Append("From nobody Fri Jan  11 07:00:00 1980\n");
            sb.Append("Content-Type: multipart/mixed; boundary=\"==BOUNDARY==\"\n");
            sb.Append("MIME-Version: 1.0\n");

            foreach (var data in userData)
            {
                sb.Append("--==BOUNDARY==\n");
                sb.Append("MIME-Version: 1.0\n");


                sb.Append($"Content-Type: {data.ContentType.Name}; charset=\"{data.Encoding.BodyName}\"\n");

                if(!string.IsNullOrWhiteSpace(data.FileName))
                    sb.Append($"Content-Disposition: attachment; filename=\"{data.FileName}\"\n");

                var contentString = data.Content.Replace("\r\n", "\n");
                if (options.Base64Encode)
                {
                    contentString = Convert
                        .ToBase64String(data.Encoding.GetBytes(contentString), Base64FormattingOptions.InsertLineBreaks)
                        .Replace("\r\n", "\n");
                    sb.Append("Content-Transfer-Encoding: base64\n");
                }

                sb.Append(contentString+ "\n");


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