using System.Runtime.InteropServices;

namespace Haipa.CloudInit.ConfigDrive.Interop
{
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [CoClass(typeof(MsftDiscRecorder2Class)), ComImport]
    [Guid("27354132-7F64-5B0F-8F00-5D77AFBE261E")]
    public interface MsftDiscRecorder2Ex : IDiscRecorder2Ex
    {
    }
}