using System;

namespace Contiva.CloudInit.ConfigDrive.Interop
{
    [Flags]
    public enum FsiItemType
    {
        FsiItemNotFound = 0,
        FsiItemDirectory = 1,
        FsiItemFile = 2,
    }
}