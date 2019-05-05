using System;

namespace Haipa.CloudInit.ConfigDrive.Interop
{
    [Flags]
    public enum FsiItemType
    {
        FsiItemNotFound = 0,
        FsiItemDirectory = 1,
        FsiItemFile = 2,
    }
}