using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Dbosoft.CloudInit.ConfigDrive
{
    public interface IYamlSerializer
    {
        Task<Stream> SerializeToYaml<T>(T data);
    }


}