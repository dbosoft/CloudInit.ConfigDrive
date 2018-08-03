using System;
using System.Runtime.InteropServices;

namespace Contiva.Windows.ImagingApi.Interop
{
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("27354132-7F64-5B0F-8F00-5D77AFBE261E")]
    public interface IDiscRecorder2Ex
    {
        /// <summary>
        /// Send a command to the device that does not transfer any data
        /// </summary>
        /// <param name="Cdb"></param>
        /// <param name="CdbSize"></param>
        /// <param name="SenseBuffer"></param>
        /// <param name="Timeout"></param>
        void SendCommandNoData([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 1)]
            Byte[] cdb,
            uint cdbSize,
            [MarshalAs(UnmanagedType.LPArray, SizeConst = 18)]
            Byte[] senseBuffer,
            uint timeout);

        /// <summary>
        /// Send a command to the device that requires data sent to the device
        /// </summary>
        /// <param name="Cdb"></param>
        /// <param name="CdbSize"></param>
        /// <param name="SenseBuffer"></param>
        /// <param name="Timeout"></param>
        /// <param name="Buffer"></param>
        /// <param name="BufferSize"></param>
        void SendCommandSendDataToDevice([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 1)]
            Byte[] cdb,
            uint cdbSize,
            [MarshalAs(UnmanagedType.LPArray, SizeConst = 18)]
            Byte[] senseBuffer,
            uint timeout,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 5)]
            Byte[] buffer,
            uint bufferSize);

        /// <summary>
        /// Send a command to the device that requests data from the device
        /// </summary>
        /// <param name="Cdb"></param>
        /// <param name="CdbSize"></param>
        /// <param name="SenseBuffer"></param>
        /// <param name="Timeout"></param>
        /// <param name="Buffer"></param>
        /// <param name="BufferSize"></param>
        /// <param name="BufferFetched"></param>
        void SendCommandGetDataFromDevice([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 1)]
            Byte[] cdb,
            uint cdbSize,
            [MarshalAs(UnmanagedType.LPArray, SizeConst = 18)]
            Byte[] senseBuffer,
            uint timeout,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 5)]
            Byte[] buffer,
            uint bufferSize,
            ref uint bufferFetched);

        /// <summary>
        /// Read a DVD Structure from the media
        /// </summary>
        /// <param name="format"></param>
        /// <param name="address"></param>
        /// <param name="layer"></param>
        /// <param name="agid"></param>
        /// <param name="data"></param>
        /// <param name="Count"></param>
        void ReadDvdStructure(uint format,
            uint address,
            uint layer,
            uint agid,
            out IntPtr dvdStructurePtr,
            ref uint count);

        /// <summary>
        /// Read a DVD Structure from the media
        /// </summary>
        /// <param name="format"></param>
        /// <param name="data"></param>
        /// <param name="Count"></param>
        void SendDvdStructure(uint format,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 2)]
            Byte[] dvdStructure,
            uint count);

        /// <summary>
        /// Get the full adapter descriptor (via IOCTL_STORAGE_QUERY_PROPERTY).
        /// </summary>
        /// <param name="data"></param>
        /// <param name="byteSize"></param>
        void GetAdapterDescriptor(out IntPtr adapterDescriptorPtr,
            ref uint byteSize);

        /// <summary>
        /// Get the full device descriptor (via IOCTL_STORAGE_QUERY_PROPERTY).
        /// </summary>
        /// <param name="data"></param>
        /// <param name="byteSize"></param>
        void GetDeviceDescriptor(out IntPtr deviceDescriptorPtr,
            ref uint byteSize);

        /// <summary>
        /// Gets data from a READ_DISC_INFORMATION command
        /// </summary>
        /// <param name="discInformation"></param>
        /// <param name="byteSize"></param>
        void GetDiscInformation(out IntPtr discInformationPtr,
            ref uint byteSize);

        /// <summary>
        /// Gets data from a READ_TRACK_INFORMATION command
        /// </summary>
        /// <param name="address"></param>
        /// <param name="addressType"></param>
        /// <param name="trackInformation"></param>
        /// <param name="byteSize"></param>
        void GetTrackInformation(uint address,
            IMAPI_READ_TRACK_ADDRESS_TYPE addressType,
            out IntPtr trackInformationPtr,
            ref uint byteSize);

        /// <summary>
        /// Gets a feature's data from a GET_CONFIGURATION command
        /// </summary>
        /// <param name="requestedFeature"></param>
        /// <param name="currentFeatureOnly"></param>
        /// <param name="featureData"></param>
        /// <param name="byteSize"></param>
        void GetFeaturePage(IMAPI_FEATURE_PAGE_TYPE requestedFeature,
            [MarshalAs(UnmanagedType.U1)]
            bool currentFeatureOnly,
            out IntPtr featureDataPtr,
            ref uint byteSize);

        /// <summary>
        /// Gets data from a MODE_SENSE10 command
        /// </summary>
        /// <param name="requestedModePage"></param>
        /// <param name="requestType"></param>
        /// <param name="modePageData"></param>
        /// <param name="byteSize"></param>
        void GetModePage(IMAPI_MODE_PAGE_TYPE requestedModePage,
            IMAPI_MODE_PAGE_REQUEST_TYPE requestType,
            out IntPtr modePageDataPtr,
            ref uint byteSize);

        /// <summary>
        /// Sets mode page data using MODE_SELECT10 command
        /// </summary>
        /// <param name="requestType"></param>
        /// <param name="data"></param>
        /// <param name="byteSize"></param>
        void SetModePage(IMAPI_MODE_PAGE_REQUEST_TYPE requestType,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 2)]
            Byte[] modePage,
            uint byteSize);

        /// <summary>
        /// Gets a list of all feature pages supported by the device
        /// </summary>
        /// <param name="currentFeatureOnly"></param>
        /// <param name="featureData"></param>
        /// <param name="byteSize"></param>
        void GetSupportedFeaturePages([MarshalAs(UnmanagedType.U1)]
            bool currentFeatureOnly,
            out IntPtr featureDataPtr,
            ref uint byteSize);

        /// <summary>
        /// Gets a list of all PROFILES supported by the device
        /// </summary>
        /// <param name="currentOnly"></param>
        /// <param name="profileTypes"></param>
        /// <param name="validProfiles"></param>
        void GetSupportedProfiles([MarshalAs(UnmanagedType.U1)]
            bool currentOnly,
            out IntPtr profileTypesPtr,
            ref uint validProfiles);

        /// <summary>
        /// Gets a list of all MODE PAGES supported by the device
        /// </summary>
        /// <param name="requestType"></param>
        /// <param name="modePageTypes"></param>
        /// <param name="validPages"></param>
        void GetSupportedModePages(IMAPI_MODE_PAGE_REQUEST_TYPE requestType,
            out IntPtr modePageTypesPtr,
            ref uint validPages);

        /// <summary>
        /// The byte alignment requirement mask for this device.
        /// </summary>
        /// <returns></returns>
        uint GetByteAlignmentMask();

        /// <summary>
        /// The maximum non-page-aligned transfer size for this device.
        /// </summary>
        /// <returns></returns>
        uint GetMaximumNonPageAlignedTransferSize();

        /// <summary>
        /// The maximum non-page-aligned transfer size for this device.
        /// </summary>
        /// <returns></returns>
        uint GetMaximumPageAlignedTransferSize();
    }
}