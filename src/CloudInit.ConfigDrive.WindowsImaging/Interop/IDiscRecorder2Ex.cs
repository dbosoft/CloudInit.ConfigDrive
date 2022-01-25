// copyright dbosoft GmbH / licensed under MIT license
// based on sample code by Microsoft Windows SDK samples 
// https://github.com/microsoft/Windows-classic-samples/blob/master/Samples/Win7Samples/winbase/imapi/interop/IMAPIv2.cs


using System;
using System.Runtime.InteropServices;

// ReSharper disable UnusedMember.Global
#pragma warning disable 1584,1711,1572,1581,1580

namespace Dbosoft.CloudInit.ConfigDrive.Interop
{
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("27354132-7F64-5B0F-8F00-5D77AFBE261E")]
    public interface IDiscRecorder2Ex
    {
        /// <summary>
        /// Send a command to the device that does not transfer any data
        /// </summary>
        /// <param name="cdb"></param>
        /// <param name="cdbSize"></param>
        /// <param name="senseBuffer"></param>
        /// <param name="timeout"></param>
        void SendCommandNoData([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 1)]
            byte[] cdb,
            uint cdbSize,
            [MarshalAs(UnmanagedType.LPArray, SizeConst = 18)]
            byte[] senseBuffer,
            uint timeout);

        /// <summary>
        /// Send a command to the device that requires data sent to the device
        /// </summary>
        /// <param name="cdb"></param>
        /// <param name="cdbSize"></param>
        /// <param name="senseBuffer"></param>
        /// <param name="timeout"></param>
        /// <param name="buffer"></param>
        /// <param name="bufferSize"></param>
        void SendCommandSendDataToDevice([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 1)]
            byte[] cdb,
            uint cdbSize,
            [MarshalAs(UnmanagedType.LPArray, SizeConst = 18)]
            byte[] senseBuffer,
            uint timeout,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 5)]
            byte[] buffer,
            uint bufferSize);

        /// <summary>
        /// Send a command to the device that requests data from the device
        /// </summary>
        /// <param name="cdb"></param>
        /// <param name="cdbSize"></param>
        /// <param name="senseBuffer"></param>
        /// <param name="timeout"></param>
        /// <param name="buffer"></param>
        /// <param name="bufferSize"></param>
        /// <param name="bufferFetched"></param>
        void SendCommandGetDataFromDevice([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 1)]
            byte[] cdb,
            uint cdbSize,
            [MarshalAs(UnmanagedType.LPArray, SizeConst = 18)]
            byte[] senseBuffer,
            uint timeout,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 5)]
            byte[] buffer,
            uint bufferSize,
            ref uint bufferFetched);

        void ReadDvdStructure(uint format,
            uint address,
            uint layer,
            uint agid,
            out IntPtr dvdStructurePtr,
            ref uint count);

        void SendDvdStructure(uint format,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 2)]
            byte[] dvdStructure,
            uint count);

        void GetAdapterDescriptor(out IntPtr adapterDescriptorPtr,
            ref uint byteSize);

        void GetDeviceDescriptor(out IntPtr deviceDescriptorPtr,
            ref uint byteSize);

        void GetDiscInformation(out IntPtr discInformationPtr,
            ref uint byteSize);

        void GetTrackInformation(uint address,
            IMAPI_READ_TRACK_ADDRESS_TYPE addressType,
            out IntPtr trackInformationPtr,
            ref uint byteSize);

        void GetFeaturePage(IMAPI_FEATURE_PAGE_TYPE requestedFeature,
            [MarshalAs(UnmanagedType.U1)]
            bool currentFeatureOnly,
            out IntPtr featureDataPtr,
            ref uint byteSize);

        void GetModePage(IMAPI_MODE_PAGE_TYPE requestedModePage,
            IMAPI_MODE_PAGE_REQUEST_TYPE requestType,
            out IntPtr modePageDataPtr,
            ref uint byteSize);

        void SetModePage(IMAPI_MODE_PAGE_REQUEST_TYPE requestType,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 2)]
            byte[] modePage,
            uint byteSize);

        void GetSupportedFeaturePages([MarshalAs(UnmanagedType.U1)]
            bool currentFeatureOnly,
            out IntPtr featureDataPtr,
            ref uint byteSize);

        void GetSupportedProfiles([MarshalAs(UnmanagedType.U1)]
            bool currentOnly,
            out IntPtr profileTypesPtr,
            ref uint validProfiles);

        void GetSupportedModePages(IMAPI_MODE_PAGE_REQUEST_TYPE requestType,
            out IntPtr modePageTypesPtr,
            ref uint validPages);

        uint GetByteAlignmentMask();

        uint GetMaximumNonPageAlignedTransferSize();

        uint GetMaximumPageAlignedTransferSize();
    }
}