using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Contiva.Windows.ImagingApi.Interop
{
    [TypeLibType(4480)]
    [Guid("2735413C-7F64-5B0F-8F00-5D77AFBE261E"), ComImport]
    public interface DDiscFormat2DataEvents
    {
        [DispId(0x200)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Update([In, MarshalAs(UnmanagedType.IDispatch)] object sender, [In, MarshalAs(UnmanagedType.IDispatch)] object progress);
    }
}