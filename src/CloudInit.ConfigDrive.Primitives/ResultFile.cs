using System.IO;

namespace Dbosoft.CloudInit.ConfigDrive
{
    public class ResultFile
    {
        public ResultFile(string path, Stream content)
        {
            Path = path;
            Content = content;
        }

        public string Path { get; set; }
        public Stream Content { get; set; }
    }
}