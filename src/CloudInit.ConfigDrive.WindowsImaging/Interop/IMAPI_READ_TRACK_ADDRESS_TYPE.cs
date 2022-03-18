// copyright dbosoft GmbH / licensed under MIT license
// based on sample code by Microsoft Windows SDK samples 
// https://github.com/microsoft/Windows-classic-samples/blob/master/Samples/Win7Samples/winbase/imapi/interop/IMAPIv2.cs


// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global

namespace Dbosoft.CloudInit.ConfigDrive.Interop
{
    public enum IMAPI_READ_TRACK_ADDRESS_TYPE
    {
        IMAPI_READ_TRACK_ADDRESS_TYPE_LBA = 0,
        IMAPI_READ_TRACK_ADDRESS_TYPE_TRACK = 1,
        IMAPI_READ_TRACK_ADDRESS_TYPE_SESSION = 2,
    }
}