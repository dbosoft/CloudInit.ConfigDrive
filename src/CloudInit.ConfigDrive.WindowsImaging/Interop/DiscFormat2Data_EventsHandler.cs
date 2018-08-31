using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming

namespace Contiva.CloudInit.ConfigDrive.Interop
{
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void DiscFormat2Data_EventsHandler([In, MarshalAs(UnmanagedType.IDispatch)] object sender, [In, MarshalAs(UnmanagedType.IDispatch)] object args);
}