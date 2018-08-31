using System.Runtime.InteropServices;

namespace Contiva.CloudInit.ConfigDrive.Interop
{
    [CoClass(typeof(MsftDiscRecorder2Class)), ComImport]
    [Guid("27354133-7F64-5B0F-8F00-5D77AFBE261E")]
    public interface MsftDiscRecorder2 : IDiscRecorder2
    {
    }
}