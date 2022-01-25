// copyright dbosoft GmbH / licensed under MIT license
// based on sample code by Microsoft Windows SDK samples 
// https://github.com/microsoft/Windows-classic-samples/blob/master/Samples/Win7Samples/winbase/imapi/interop/IMAPIv2.cs

using System.Runtime.InteropServices;

namespace Dbosoft.CloudInit.ConfigDrive.Interop
{
#pragma warning disable 618
    [TypeLibType((short)0x10), ComVisible(false), ComEventInterface(typeof(DDiscFormat2DataEvents), typeof(DiscFormat2Data_EventsProvider))]
#pragma warning restore 618
    public interface DiscFormat2Data_Events
    {
        event DiscFormat2Data_EventsHandler Update;
    }
}