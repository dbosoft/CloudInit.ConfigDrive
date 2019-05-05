using System.Runtime.InteropServices;

namespace Haipa.CloudInit.ConfigDrive.Interop
{
    [CoClass(typeof(MsftBootOptionsClass)), ComImport]
    [Guid("2C941FD4-975B-59BE-A960-9A2A262853A5")]
    public interface MsftBootOptions : IBootOptions
    {
    }
}