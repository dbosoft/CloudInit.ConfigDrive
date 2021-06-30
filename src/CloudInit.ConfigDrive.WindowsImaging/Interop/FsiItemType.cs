// copyright dbosoft GmbH / licensed under MIT license
// based on sample code by Microsoft Windows SDK samples 
// https://github.com/microsoft/Windows-classic-samples/blob/master/Samples/Win7Samples/winbase/imapi/interop/IMAPIv2.cs


using System;

namespace Dbosoft.CloudInit.ConfigDrive.Interop
{
    [Flags]
    public enum FsiItemType
    {
        FsiItemNotFound = 0,
        FsiItemDirectory = 1,
        FsiItemFile = 2,
    }
}