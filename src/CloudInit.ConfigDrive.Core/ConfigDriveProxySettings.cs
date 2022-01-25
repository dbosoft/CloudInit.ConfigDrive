using JetBrains.Annotations;

namespace Dbosoft.CloudInit.ConfigDrive
{
    
    [PublicAPI]
    public struct ConfigDriveProxySettings
    {
        public string HttpProxy { get; set; }
        public string HttpsProxy { get; set; }
        public string FtpProxy { get; set; }

        public string NoProxy { get; set; }

        public ConfigDriveProxySettings(string proxy = "")
        {
            HttpProxy = proxy;
            HttpsProxy = proxy;
            FtpProxy = proxy;
            NoProxy = "";
        }

    }
}