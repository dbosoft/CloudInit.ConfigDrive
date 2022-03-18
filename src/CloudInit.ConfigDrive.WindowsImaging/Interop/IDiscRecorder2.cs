// copyright dbosoft GmbH / licensed under MIT license
// based on sample code by Microsoft Windows SDK samples 
// https://github.com/microsoft/Windows-classic-samples/blob/master/Samples/Win7Samples/winbase/imapi/interop/IMAPIv2.cs


using System.Runtime.InteropServices;

namespace Dbosoft.CloudInit.ConfigDrive.Interop
{
    [TypeLibType(TypeLibTypeFlags.FDual |
                 TypeLibTypeFlags.FDispatchable |
                 TypeLibTypeFlags.FNonExtensible)]
    [Guid("27354133-7F64-5B0F-8F00-5D77AFBE261E"), ComImport]
    public interface IDiscRecorder2
    {
        /// <summary>
        /// Ejects the media (if any) and opens the tray
        /// </summary>
        [DispId(256)]
        void EjectMedia();

        /// <summary>
        /// Close the media tray and load any media in the tray.
        /// </summary>
        [DispId(257)]
        void CloseTray();

        /// <summary>
        /// Acquires exclusive access to device. 
        /// May be called multiple times.
        /// </summary>
        /// <param name="force"></param>
        /// <param name="clientName">App Id</param>
        [DispId(258)]
        void AcquireExclusiveAccess([MarshalAs(UnmanagedType.VariantBool)] bool force, [MarshalAs(UnmanagedType.BStr)] string clientName);

        /// <summary>
        /// Releases exclusive access to device. 
        /// Call once per AcquireExclusiveAccess().
        /// </summary>
        [DispId(259)]
        void ReleaseExclusiveAccess();

        /// <summary>
        /// Disables Media Change Notification (MCN).
        /// </summary>
        [DispId(260)]
        void DisableMcn();

        /// <summary>
        /// Re-enables Media Change Notification after a call to DisableMcn()
        /// </summary>
        [DispId(261)]
        void EnableMcn();

        /// <summary>
        /// Initialize the recorder, opening a handle to the specified recorder.
        /// </summary>
        /// <param name="recorderUniqueId"></param>
        [DispId(262)]
        void InitializeDiscRecorder([MarshalAs(UnmanagedType.BStr)] string recorderUniqueId);

        /// <summary>
        /// The unique ID used to initialize the recorder.
        /// </summary>
        [DispId(0)]
        string ActiveDiscRecorder { [return: MarshalAs(UnmanagedType.BStr)] get; }

        /// <summary>
        /// The vendor ID in the device's INQUIRY data.
        /// </summary>
        [DispId(513)]
        string VendorId { [return: MarshalAs(UnmanagedType.BStr)] get; }

        /// <summary>
        /// The Product ID in the device's INQUIRY data.
        /// </summary>
        [DispId(514)]
        string ProductId { [return: MarshalAs(UnmanagedType.BStr)] get; }

        /// <summary>
        /// The Product Revision in the device's INQUIRY data.
        /// </summary>
        [DispId(515)]
        string ProductRevision { [return: MarshalAs(UnmanagedType.BStr)] get; }

        /// <summary>
        /// Get the unique volume name (this is not a drive letter).
        /// </summary>
        [DispId(516)]
        string VolumeName { [return: MarshalAs(UnmanagedType.BStr)] get; }

        /// <summary>
        /// Drive letters and NTFS mount points to access the recorder.
        /// </summary>
        [DispId(517)]
        string[] VolumePathNames { [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_VARIANT)] get; }

        /// <summary>
        /// One of the volume names associated with the recorder.
        /// </summary>
        [DispId(518)]
        bool DeviceCanLoadMedia { [return: MarshalAs(UnmanagedType.VariantBool)] get; }

        /// <summary>
        /// Gets the legacy 'device number' associated with the recorder. 
        /// This number is not guaranteed to be static.
        /// </summary>
        [DispId(519)]
        int LegacyDeviceNumber { get; }

        /// <summary>
        /// Gets a list of all feature pages supported by the device
        /// </summary>
        [DispId(520)]
        int[] SupportedFeaturePages { [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_VARIANT)] get; }

        /// <summary>
        /// Gets a list of all feature pages with 'current' bit set to true
        /// </summary>
        [DispId(521)]
        int[] CurrentFeaturePages { [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_VARIANT)] get; }

        /// <summary>
        /// Gets a list of all profiles supported by the device
        /// </summary>
        [DispId(522)]
        int[] SupportedProfiles { [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_VARIANT)] get; }

        /// <summary>
        /// Gets a list of all profiles with 'currentP' bit set to true
        /// </summary>
        [DispId(523)]
        int[] CurrentProfiles { [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_VARIANT)] get; }

        /// <summary>
        /// Gets a list of all MODE PAGES supported by the device
        /// </summary>
        [DispId(524)]
        int[] SupportedModePages { [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_VARIANT)] get; }

        /// <summary>
        /// Queries the device to determine who, if anyone, has acquired exclusive access
        /// </summary>
        [DispId(525)]
        string ExclusiveAccessOwner { [return: MarshalAs(UnmanagedType.BStr)] get; }
    }
}