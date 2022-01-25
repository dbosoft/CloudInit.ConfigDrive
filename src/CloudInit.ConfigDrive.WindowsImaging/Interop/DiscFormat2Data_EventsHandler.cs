// copyright dbosoft GmbH / licensed under MIT license
// based on sample code by Microsoft Windows SDK samples 
// https://github.com/microsoft/Windows-classic-samples/blob/master/Samples/Win7Samples/winbase/imapi/interop/IMAPIv2.cs

using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming

namespace Dbosoft.CloudInit.ConfigDrive.Interop
{
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void DiscFormat2Data_EventsHandler([In, MarshalAs(UnmanagedType.IDispatch)] object sender, [In, MarshalAs(UnmanagedType.IDispatch)] object args);
}