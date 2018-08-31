using System;

namespace Contiva.CloudInit.ConfigDrive.Interop
{ 
    [Flags]
    public enum EmulationType
    {
        EmulationNone = 0,
        Emulation12MFloppy = 1,
        Emulation144MFloppy = 2,
        Emulation288MFloppy = 3,
        EmulationHardDisk = 4,
    }
}