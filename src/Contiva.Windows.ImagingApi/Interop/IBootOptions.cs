using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Contiva.Windows.ImagingApi.Interop
{
    [TypeLibType(TypeLibTypeFlags.FDual |
                 TypeLibTypeFlags.FDispatchable |
                 TypeLibTypeFlags.FNonExtensible)]
    [Guid("2C941FD4-975B-59BE-A960-9A2A262853A5")]
    public interface IBootOptions
    {
        /// <summary>
        /// Get boot image data stream
        /// </summary>
        [DispId(1)]
        IStream BootImage { get; }

        /// <summary>
        /// Get boot manufacturer
        /// </summary>
        [DispId(2)]
        String Manufacturer { set; [return: MarshalAs(UnmanagedType.BStr)] get; }

        /// <summary>
        /// Get boot platform identifier
        /// </summary>
        [DispId(3)]
        PlatformId PlatformId { get; set; }

        /// <summary>
        /// Get boot emulation type
        /// </summary>
        [DispId(4)]
        EmulationType Emulation { get; set; }

        /// <summary>
        /// Get boot image size
        /// </summary>
        [DispId(5)]
        uint ImageSize { get; }

        /// <summary>
        /// Set the boot image data stream, emulation type, and image size
        /// </summary>
        /// <param name="newVal"></param>
        [DispId(20)]
        void AssignBootImage(IStream newVal);
    }
}