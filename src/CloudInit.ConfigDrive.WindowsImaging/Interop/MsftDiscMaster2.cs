using System.Runtime.InteropServices;

namespace Haipa.CloudInit.ConfigDrive.Interop
{
    [CoClass(typeof(MsftDiscMaster2Class)), ComImport]
    [Guid("27354130-7F64-5B0F-8F00-5D77AFBE261E")]
    public interface MsftDiscMaster2 : IDiscMaster2
    {
    }
}