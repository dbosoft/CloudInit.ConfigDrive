using System.Runtime.InteropServices;

namespace Haipa.CloudInit.ConfigDrive.Interop
{
    [TypeLibType((short)0x10), ComVisible(false), ComEventInterface(typeof(DDiscFormat2DataEvents), typeof(DiscFormat2Data_EventsProvider))]
    public interface DiscFormat2Data_Events
    {
        event DiscFormat2Data_EventsHandler Update;
    }
}