using System.IO;

namespace Haipa.CloudInit.ConfigDrive
{
    public class ResultFile
    {
        public string Path { get; set; }
        public Stream Content { get; set; }
    }
}