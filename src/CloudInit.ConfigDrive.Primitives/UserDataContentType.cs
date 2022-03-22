namespace Dbosoft.CloudInit.ConfigDrive
{
    public readonly struct UserDataContentType
    {
        private UserDataContentType(string contentType)
        {
            Name = contentType;
        }

        public readonly string Name;


        public static UserDataContentType IncludeUrl = new UserDataContentType("text/x-include-url");
        public static UserDataContentType IncludeUrlOnce = new UserDataContentType("text/x-include-once-url");
        public static UserDataContentType CloudConfigArchive = new UserDataContentType("text/cloud-config-archive");
        public static UserDataContentType UpstartJob = new UserDataContentType("text/upstart-job");
        public static UserDataContentType CloudConfig = new UserDataContentType("text/cloud-config");
        public static UserDataContentType PartHandler = new UserDataContentType("text/part-handler");
        public static UserDataContentType ShellScript = new UserDataContentType("text/x-shellscript");
        public static UserDataContentType BootHook = new UserDataContentType("text/cloud-boothook");

    }
}