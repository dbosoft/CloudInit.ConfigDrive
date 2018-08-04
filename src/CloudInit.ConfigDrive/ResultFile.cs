using System.IO;

namespace Contiva.CloudInit.ConfigDrive
{
    public class ResultFile
    {
        public string Path { get; set; }
        public Stream Content { get; set; }
    }
}