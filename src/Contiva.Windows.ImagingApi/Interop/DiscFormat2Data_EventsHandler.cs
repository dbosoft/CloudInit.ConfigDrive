using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace Contiva.Windows.ImagingApi.Interop
{
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void DiscFormat2Data_EventsHandler([In, MarshalAs(UnmanagedType.IDispatch)] object sender, [In, MarshalAs(UnmanagedType.IDispatch)] object args);
}