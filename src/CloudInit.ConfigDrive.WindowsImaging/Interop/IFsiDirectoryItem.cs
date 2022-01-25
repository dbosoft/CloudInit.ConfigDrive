// copyright dbosoft GmbH / licensed under MIT license
// based on sample code by Microsoft Windows SDK samples 
// https://github.com/microsoft/Windows-classic-samples/blob/master/Samples/Win7Samples/winbase/imapi/interop/IMAPIv2.cs


using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

// ReSharper disable UnusedMember.Global

namespace Dbosoft.CloudInit.ConfigDrive.Interop
{
    [TypeLibType(TypeLibTypeFlags.FDual |
                 TypeLibTypeFlags.FDispatchable |
                 TypeLibTypeFlags.FNonExtensible)]
    [Guid("2C941FDC-975B-59BE-A960-9A2A262853A5")]
    public interface IFsiDirectoryItem
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

        // IFsiDirectoryItem
        /// <summary>
        /// Get an enumerator for the collection
        /// </summary>
        /// <returns></returns>
        [DispId(-4)]
        [TypeLibFunc(65)]
        IEnumerator GetEnumerator();

        /// <summary>
        /// Get the item with the given relative path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [DispId(0)]
        IFsiItem this[string path] { get; }

        /// <summary>
        /// Number of items in the collection
        /// </summary>
        [DispId(1)]
        int Count { get; }

        /// <summary>
        /// Get a non-variant enumerator
        /// </summary>
        [DispId(2)]
        IEnumFsiItems EnumFsiItems { get; }

        /// <summary>
        /// Add a directory with the specified relative path
        /// </summary>
        /// <param name="path"></param>
        [DispId(30)]
        void AddDirectory(string path);

        /// <summary>
        /// Add a file with the specified relative path and data
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileData"></param>
        [DispId(31)]
        void AddFile(string path, IStream fileData);

        /// <summary>
        /// Add files and directories from the specified source directory
        /// </summary>
        /// <param name="sourceDirectory"></param>
        /// <param name="includeBaseDirectory"></param>
        [DispId(32)]
        void AddTree(string sourceDirectory, bool includeBaseDirectory);

        /// <summary>
        /// Add an item
        /// </summary>
        /// <param name="Item"></param>
        [DispId(33)]
        // ReSharper disable once InconsistentNaming
        void Add(IFsiItem Item);

        /// <summary>
        /// Remove an item with the specified relative path
        /// </summary>
        /// <param name="path"></param>
        [DispId(34)]
        void Remove(string path);

        /// <summary>
        /// Remove a subtree with the specified relative path
        /// </summary>
        /// <param name="path"></param>
        [DispId(35)]
        void RemoveTree(string path);
    }
}