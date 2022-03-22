using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Dbosoft.CloudInit.ConfigDrive
{
    public class UserData
    {
        public UserData(UserDataContentType contentType, string content, Encoding encoding)
        {
            ContentType = contentType;
            Content = content;
            Encoding = encoding;
        }

        public UserDataContentType ContentType { get; set; }
        public string Content { get; set; }
        public Encoding Encoding { get; set; }

    }
}