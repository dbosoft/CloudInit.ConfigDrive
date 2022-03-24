using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using JetBrains.Annotations;

namespace Dbosoft.CloudInit.ConfigDrive
{
    [PublicAPI]
    public class UserData
    {
        public UserData(UserDataContentType contentType, string content, Encoding encoding)
        {
            ContentType = contentType;
            Content = content;
            Encoding = encoding;
        }

        public UserData(UserDataContentType contentType, string content, string fileName, Encoding encoding)
        {
            ContentType = contentType;
            Content = content;
            FileName = fileName;
            Encoding = encoding;
        }

        public UserDataContentType ContentType { get; set; }
        public string Content { get;  }
        public Encoding Encoding { get;  }
        public string? FileName { get; set; }
    }
}