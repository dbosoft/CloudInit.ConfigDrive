// copyright dbosoft GmbH / licensed under MIT license
// based on sample code by Microsoft Windows SDK samples 
// https://github.com/microsoft/Windows-classic-samples/blob/master/Samples/Win7Samples/winbase/imapi/interop/IMAPIv2.cs

// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming

using System;

#pragma warning disable 1591
namespace Dbosoft.CloudInit.ConfigDrive.Interop
{
    [Flags]
    public enum FsiFileSystems
    {
        FsiFileSystemNone = 0,
        FsiFileSystemISO9660 = 1,
        FsiFileSystemJoliet = 2,
        FsiFileSystemUDF = 4,
        FsiFileSystemUnknown = 1073741824,
    }
}