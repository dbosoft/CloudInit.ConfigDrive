// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming

using System;

#pragma warning disable 1591
namespace Haipa.CloudInit.ConfigDrive.Interop
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