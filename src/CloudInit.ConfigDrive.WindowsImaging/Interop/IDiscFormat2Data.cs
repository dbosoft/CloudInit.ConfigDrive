// copyright dbosoft GmbH / licensed under MIT license
// based on sample code by Microsoft Windows SDK samples 
// https://github.com/microsoft/Windows-classic-samples/blob/master/Samples/Win7Samples/winbase/imapi/interop/IMAPIv2.cs


using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global

namespace Dbosoft.CloudInit.ConfigDrive.Interop
{
    [TypeLibType(TypeLibTypeFlags.FDual |
                 TypeLibTypeFlags.FDispatchable |
                 TypeLibTypeFlags.FNonExtensible)]
    [Guid("27354153-9F64-5B0F-8F00-5D77AFBE261E")]
    public interface IDiscFormat2Data
    {
        // IDiscFormat2
        /// <summary>
        /// Determines if the recorder object supports the given format
        /// </summary>
        /// <param name="Recorder"></param>
        /// <returns></returns>
        [DispId(2048)]
        [return: MarshalAs(UnmanagedType.VariantBool)]
        bool IsRecorderSupported(IDiscRecorder2 Recorder);

        /// <summary>
        /// Determines if the current media in a supported recorder object 
        /// supports the given format
        /// </summary>
        /// <param name="Recorder"></param>
        /// <returns></returns>
        [DispId(2049)]
        [return: MarshalAs(UnmanagedType.VariantBool)]
        bool IsCurrentMediaSupported(IDiscRecorder2 Recorder);

        /// <summary>
        /// Determines if the current media is reported as physically blank 
        /// by the drive
        /// </summary>
        [DispId(1792)]
        bool MediaPhysicallyBlank { [return: MarshalAs(UnmanagedType.VariantBool)] get; }

        /// <summary>
        /// Attempts to determine if the media is blank using heuristics 
        /// (mainly for DVD+RW and DVD-RAM media)
        /// </summary>
        [DispId(1793)]
        bool MediaHeuristicallyBlank { [return: MarshalAs(UnmanagedType.VariantBool)] get; }

        /// <summary>
        /// Supported media types
        /// </summary>
        [DispId(1794)]
        int[] SupportedMediaTypes { [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_VARIANT)] get; }

        // IDiscFormat2Data
        /// <summary>
        /// The disc recorder to use
        /// </summary>
        [DispId(256)]
        IDiscRecorder2 Recorder { set; [return: MarshalAs(UnmanagedType.Interface)] get; }

        /// <summary>
        /// Buffer Underrun Free recording should be disabled
        /// </summary>
        [DispId(257)]
        bool BufferUnderrunFreeDisabled { set; [return: MarshalAs(UnmanagedType.VariantBool)] get; }

        /// <summary>
        /// Postgap is included in image
        /// </summary>
        [DispId(260)]
        bool PostgapAlreadyInImage { set; [return: MarshalAs(UnmanagedType.VariantBool)] get; }

        /// <summary>
        /// The state (usability) of the current media
        /// </summary>
        [DispId(262)]
        IMAPI_FORMAT2_DATA_MEDIA_STATE CurrentMediaStatus { get; }

        /// <summary>
        /// The write protection state of the current media.
        /// </summary>
        [DispId(263)]
        IMAPI_MEDIA_WRITE_PROTECT_STATE WriteProtectStatus { get; }

        /// <summary>
        /// Total sectors available on the media (used + free).
        /// </summary>
        [DispId(264)]
        int TotalSectorsOnMedia { get; }

        /// <summary>
        /// Free sectors available on the media.
        /// </summary>
        [DispId(265)]
        int FreeSectorsOnMedia { get; }

        /// <summary>
        /// Next writable address on the media (also used sectors).
        /// </summary>
        [DispId(266)]
        int NextWritableAddress { get; }

        /// <summary>
        /// The first sector in the previous session on the media.
        /// </summary>
        [DispId(267)]
        int StartAddressOfPreviousSession { get; }

        /// <summary>
        /// The last sector in the previous session on the media.
        /// </summary>
        [DispId(268)]
        int LastWrittenAddressOfPreviousSession { get; }

        /// <summary>
        /// Prevent further additions to the file system
        /// </summary>
        [DispId(269)]
        bool ForceMediaToBeClosed { set; [return: MarshalAs(UnmanagedType.VariantBool)] get; }

        /// <summary>
        /// Default is to maximize compatibility with DVD-ROM. 
        /// May be disabled to reduce time to finish writing the disc or 
        /// increase usable space on the media for later writing.
        /// </summary>
        [DispId(270)]
        bool DisableConsumerDvdCompatibilityMode { set; [return: MarshalAs(UnmanagedType.VariantBool)] get; }

        /// <summary>
        /// Get the current physical media type.
        /// </summary>
        [DispId(271)]
        IMAPI_MEDIA_PHYSICAL_TYPE CurrentPhysicalMediaType { get; }

        /// <summary>
        /// The friendly name of the client 
        /// (used to determine recorder reservation conflicts).
        /// </summary>
        [DispId(272)]
        string ClientName { set; [return: MarshalAs(UnmanagedType.BStr)] get; }

        /// <summary>
        /// The last requested write speed.
        /// </summary>
        [DispId(273)]
        int RequestedWriteSpeed { get; }

        /// <summary>
        /// The last requested rotation type.
        /// </summary>
        [DispId(274)]
        bool RequestedRotationTypeIsPureCAV { [return: MarshalAs(UnmanagedType.VariantBool)] get; }

        /// <summary>
        /// The drive's current write speed.
        /// </summary>
        [DispId(275)]
        int CurrentWriteSpeed { get; }

        /// <summary>
        /// The drive's current rotation type.
        /// </summary>
        [DispId(276)]
        bool CurrentRotationTypeIsPureCAV { [return: MarshalAs(UnmanagedType.VariantBool)] get; }

        /// <summary>
        /// Gets an array of the write speeds supported for the 
        /// attached disc recorder and current media
        /// </summary>
        [DispId(277)]
        uint[] SupportedWriteSpeeds { [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_VARIANT)] get; }

        /// <summary>
        /// Gets an array of the detailed write configurations 
        /// supported for the attached disc recorder and current media
        /// </summary>
        [DispId(278)]
        Array SupportedWriteSpeedDescriptors { [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_VARIANT)] get; }

        /// <summary>
        /// Forces the Datawriter to overwrite the disc on overwritable media types
        /// </summary>
        [DispId(279)]
        bool ForceOverwrite { set; [return: MarshalAs(UnmanagedType.VariantBool)] get; }

        /// <summary>
        /// Returns the array of available multi-session interfaces. 
        /// The array shall not be empty
        /// </summary>
        [DispId(280)]
        Array MultisessionInterfaces { [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_VARIANT)] get; }

        /// <summary>
        /// Writes all the data provided in the IStream to the device
        /// </summary>
        /// <param name="data"></param>
        [DispId(512)]
        void Write([In, MarshalAs(UnmanagedType.Interface)] IStream data);

        /// <summary>
        /// Cancels the current write operation
        /// </summary>
        [DispId(513)]
        void CancelWrite();

        /// <summary>
        /// Sets the write speed (in sectors per second) of the attached disc recorder
        /// </summary>
        /// <param name="RequestedSectorsPerSecond"></param>
        /// <param name="RotationTypeIsPureCAV"></param>
        [DispId(514)]
        void SetWriteSpeed(int RequestedSectorsPerSecond, [In, MarshalAs(UnmanagedType.VariantBool)] bool RotationTypeIsPureCAV);
    }
}