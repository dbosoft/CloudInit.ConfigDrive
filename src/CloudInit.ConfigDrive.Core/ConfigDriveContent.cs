using System.Collections.Generic;

namespace Dbosoft.CloudInit.ConfigDrive
{
    public class ConfigDriveContent
    {
        public string MediaName { get; set; }
        public IList<ResultFile> Files { get; }

        public ConfigDriveContent()
        {
            Files = new List<ResultFile>();
        }
    }

    // ReSharper disable once TypeParameterCanBeVariant
}