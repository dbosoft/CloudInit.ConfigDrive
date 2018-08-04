using System;

namespace Contiva.Windows.ImagingApi.Interop
{
    [Flags]
    public enum FsiItemType
    {
        FsiItemNotFound = 0,
        FsiItemDirectory = 1,
        FsiItemFile = 2,
    }
}