// copyright dbosoft GmbH / licensed under MIT license
// based on sample code by Microsoft Windows SDK samples 
// https://github.com/microsoft/Windows-classic-samples/blob/master/Samples/Win7Samples/winbase/imapi/interop/IMAPIv2.cs


using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

// ReSharper disable UnusedMember.Global

namespace Dbosoft.CloudInit.ConfigDrive.Interop
{
    [TypeLibType(TypeLibTypeFlags.FDual |
                 TypeLibTypeFlags.FDispatchable |
                 TypeLibTypeFlags.FNonExtensible)]
    [Guid("2C941FD8-975B-59BE-A960-9A2A262853A5")]
    public interface IFileSystemImageResult
    {
        /// <summary>
        /// Image stream
        /// </summary>
        [DispId(1)]
        IStream ImageStream { get; }

        /// <summary>
        /// Progress item block mapping collection
        /// </summary>
        [DispId(2)]
        IProgressItems ProgressItems { get; }

        /// <summary>
        /// Number of blocks in the result image
        /// </summary>
        [DispId(3)]
        int TotalBlocks { get; }

        /// <summary>
        /// Number of bytes in a block
        /// </summary>
        [DispId(4)]
        int BlockSize { get; }

        /// <summary>
        /// Disc Identifier (for identifing imported session of multi-session disc)
        /// </summary>
        [DispId(5)]
        string DiscId { [return: MarshalAs(UnmanagedType.BStr)] get; }
    }
}