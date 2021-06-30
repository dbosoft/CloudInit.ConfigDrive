// copyright dbosoft GmbH / licensed under MIT license
// based on sample code by Microsoft Windows SDK samples 
// https://github.com/microsoft/Windows-classic-samples/blob/master/Samples/Win7Samples/winbase/imapi/interop/IMAPIv2.cs


using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

// ReSharper disable UnusedMember.Global

namespace Dbosoft.CloudInit.ConfigDrive.Interop
{
    [TypeLibType(TypeLibTypeFlags.FDual |
                 TypeLibTypeFlags.FDispatchable |
                 TypeLibTypeFlags.FNonExtensible)]
    [Guid("2C941FDB-975B-59BE-A960-9A2A262853A5")]
    public interface IFsiFileItem
    {
        // IFsiItem
        /// <summary>
        /// Item name
        /// </summary>
        [DispId(11)]
        string Name { [return: MarshalAs(UnmanagedType.BStr)] get; }

        /// <summary>
        /// Path
        /// </summary>
        [DispId(12)]
        string FullPath { [return: MarshalAs(UnmanagedType.BStr)] get; }

        /// <summary>
        /// Date and time of creation
        /// </summary>
        [DispId(13)]
        DateTime CreationTime { get; set; }

        /// <summary>
        /// Date and time of last access
        /// </summary>
        [DispId(14)]
        DateTime LastAccessedTime { get; set; }

        /// <summary>
        /// Date and time of last modification
        /// </summary>
        [DispId(15)]
        DateTime LastModifiedTime { get; set; }

        /// <summary>
        /// Flag indicating if item is hidden
        /// </summary>
        [DispId(16)]
        bool IsHidden { set; [return: MarshalAs(UnmanagedType.VariantBool)] get; }

        /// <summary>
        /// Name of item in the specified file system
        /// </summary>
        /// <param name="fileSystem"></param>
        /// <returns></returns>
        [DispId(17)]
        string FileSystemName(FsiFileSystems fileSystem);

        /// <summary>
        /// Name of item in the specified file system
        /// </summary>
        /// <param name="fileSystem"></param>
        /// <returns></returns>
        [DispId(18)]
        string FileSystemPath(FsiFileSystems fileSystem);

        // IFsiFileItem
        /// <summary>
        /// Data byte count
        /// </summary>
        [DispId(41)]
        long DataSize { get; }

        /// <summary>
        /// Lower 32 bits of the data byte count
        /// </summary>
        [DispId(42)]
        int DataSize32BitLow { get; }

        /// <summary>
        /// Upper 32 bits of the data byte count
        /// </summary>
        [DispId(43)]
        int DataSize32BitHigh { get; }

        /// <summary>
        /// Data stream
        /// </summary>
        [DispId(44)]
        IStream Data { get; set; }
    }
}