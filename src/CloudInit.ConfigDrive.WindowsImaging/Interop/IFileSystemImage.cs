// copyright dbosoft GmbH / licensed under MIT license
// based on sample code by Microsoft Windows SDK samples 
// https://github.com/microsoft/Windows-classic-samples/blob/master/Samples/Win7Samples/winbase/imapi/interop/IMAPIv2.cs


using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// ReSharper disable UnusedMember.Global

namespace Dbosoft.CloudInit.ConfigDrive.Interop
{
    [ComImport]
    [TypeLibType(TypeLibTypeFlags.FDual |
                 TypeLibTypeFlags.FDispatchable |
                 TypeLibTypeFlags.FNonExtensible)]
    [Guid("2C941FE1-975B-59BE-A960-9A2A262853A5")]
    public interface IFileSystemImage
    {
        /// <summary>
        /// Root directory item
        /// </summary>
        [DispId(0)]
        IFsiDirectoryItem Root { get; }

        /// <summary>
        /// Disc start block for the image
        /// </summary>
        [DispId(1)]
        int SessionStartBlock { get; set; }

        /// <summary>
        /// Maximum number of blocks available for the image
        /// </summary>
        [DispId(2)]
        int FreeMediaBlocks { get; set; }

        /// <summary>
        /// Set maximum number of blocks available based on the recorder supported discs. 
        /// 0 for unknown maximum may be set.
        /// </summary>
        /// <param name="discRecorder"></param>
        [DispId(36)]
        void SetMaxMediaBlocksFromDevice(IDiscRecorder2 discRecorder);

        /// <summary>
        /// Number of blocks in use
        /// </summary>
        [DispId(3)]
        int UsedBlocks { get; }

        /// <summary>
        /// Volume name
        /// </summary>
        [DispId(4)]
        string VolumeName { [DispId(4), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [return: MarshalAs(UnmanagedType.BStr)] get; [DispId(4), MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)] [param: MarshalAs(UnmanagedType.BStr), In] set; }

        /// <summary>
        /// Imported Volume name
        /// </summary>
        [DispId(5)]
        string ImportedVolumeName { [return: MarshalAs(UnmanagedType.BStr)] get; }

        /// <summary>
        /// Boot image and boot options
        /// </summary>
        [DispId(6)]
        IBootOptions BootImageOptions { get; set; }

        /// <summary>
        /// Number of files in the image
        /// </summary>
        [DispId(7)]
        int FileCount { get; }

        /// <summary>
        /// Number of directories in the image
        /// </summary>
        [DispId(8)]
        int DirectoryCount { get; }

        /// <summary>
        /// Temp directory for stash files
        /// </summary>
        [DispId(9)]
        string WorkingDirectory { set; [return: MarshalAs(UnmanagedType.BStr)] get; }

        /// <summary>
        /// Change point identifier
        /// </summary>
        [DispId(10)]
        int ChangePoint { get; }

        /// <summary>
        /// Strict file system compliance option
        /// </summary>
        [DispId(11)]
        bool StrictFileSystemCompliance { set; [return: MarshalAs(UnmanagedType.VariantBool)] get; }

        /// <summary>
        /// If true, indicates restricted character set is being used for file and directory names
        /// </summary>
        [DispId(12)]
        bool UseRestrictedCharacterSet { set; [return: MarshalAs(UnmanagedType.VariantBool)] get; }

        /// <summary>
        /// File systems to create
        /// </summary>
        [DispId(13)]
        FsiFileSystems FileSystemsToCreate { get; set; }

        /// <summary>
        /// File systems supported
        /// </summary>
        [DispId(14)]
        FsiFileSystems FileSystemsSupported { get; }

        /// <summary>
        /// UDF revision
        /// </summary>
        [DispId(37)]
        int UDFRevision { set; get; }

        /// <summary>
        /// UDF revision(s) supported
        /// </summary>
        [DispId(31)]
        Array UDFRevisionsSupported { get; }

        /// <summary>
        /// Select filesystem types and image size based on the current media
        /// </summary>
        /// <param name="discRecorder"></param>
        [DispId(32)]
        void ChooseImageDefaults(IDiscRecorder2 discRecorder);

        /// <summary>
        /// Select filesystem types and image size based on the media type
        /// </summary>
        /// <param name="value"></param>
        [DispId(33)]
        void ChooseImageDefaultsForMediaType(IMAPI_MEDIA_PHYSICAL_TYPE value);

        /// <summary>
        /// ISO compatibility level to create
        /// </summary>
        [DispId(34)]
        int ISO9660InterchangeLevel { set; get; }

        /// <summary>
        /// ISO compatibility level(s) supported
        /// </summary>
        [DispId(38)]
        Array ISO9660InterchangeLevelsSupported { get; }

        /// <summary>
        /// Create result image stream
        /// </summary>
        /// <returns></returns>
        [DispId(15)]
        IFileSystemImageResult CreateResultImage();

        /// <summary>
        /// Check for existance an item in the file system
        /// </summary>
        /// <param name="FullPath"></param>
        /// <returns></returns>
        [DispId(16)]
        FsiItemType Exists(string FullPath);

        /// <summary>
        /// Return a string useful for identifying the current disc
        /// </summary>
        /// <returns></returns>
        [DispId(18)]
        string CalculateDiscIdentifier();

        /// <summary>
        /// Identify file systems on a given disc
        /// </summary>
        /// <param name="discRecorder"></param>
        /// <returns></returns>
        [DispId(19)]
        FsiFileSystems IdentifyFileSystemsOnDisc(IDiscRecorder2 discRecorder);

        /// <summary>
        /// Identify which of the specified file systems would be imported by default
        /// </summary>
        /// <param name="fileSystems"></param>
        /// <returns></returns>
        [DispId(20)]
        FsiFileSystems GetDefaultFileSystemForImport(FsiFileSystems fileSystems);

        /// <summary>
        /// Import the default file system on the current disc
        /// </summary>
        /// <returns></returns>
        [DispId(21)]
        FsiFileSystems ImportFileSystem();

        /// <summary>
        /// Import a specific file system on the current disc
        /// </summary>
        /// <param name="fileSystemToUse"></param>
        [DispId(22)]
        void ImportSpecificFileSystem(FsiFileSystems fileSystemToUse);

        /// <summary>
        /// Roll back to the specified change point
        /// </summary>
        /// <param name="ChangePoint"></param>
        [DispId(23)]
        void RollbackToChangePoint(int ChangePoint);

        /// <summary>
        /// Lock in changes
        /// </summary>
        [DispId(24)]
        void LockInChangePoint();

        /// <summary>
        /// Create a directory item with the specified name
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        [DispId(25)]
        IFsiDirectoryItem CreateDirectoryItem(string Name);

        /// <summary>
        /// Create a file item with the specified name
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        [DispId(26)]
        IFsiFileItem CreateFileItem(string Name);

        /// <summary>
        /// Volume name
        /// </summary>
        [DispId(27)]
        string VolumeNameUDF { [return: MarshalAs(UnmanagedType.BStr)] get; }

        /// <summary>
        /// Volume name
        /// </summary>
        [DispId(28)]
        string VolumeNameJoliet { [return: MarshalAs(UnmanagedType.BStr)] get; }

        /// <summary>
        /// Volume name
        /// </summary>
        [DispId(29)]
        string VolumeNameISO9660 { [return: MarshalAs(UnmanagedType.BStr)] get; }

        /// <summary>
        /// Indicates whether or not IMAPI should stage the filesystem before the burn.
        /// Set to false to force IMAPI to not stage the filesystem prior to the burn.
        /// </summary>
        [DispId(30)]
        bool StageFiles { set; [return: MarshalAs(UnmanagedType.VariantBool)] get; }

        /// <summary>
        /// available multi-session interfaces.
        /// </summary>
        [DispId(40)]
        Array MultisessionInterfaces { get; set; }
    }
}