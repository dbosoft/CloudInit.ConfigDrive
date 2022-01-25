// copyright dbosoft GmbH / licensed under MIT license
// based on sample code by Microsoft Windows SDK samples 
// https://github.com/microsoft/Windows-classic-samples/blob/master/Samples/Win7Samples/winbase/imapi/interop/IMAPIv2.cs


// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global

namespace Dbosoft.CloudInit.ConfigDrive.Interop
{
    public enum IMAPI_MODE_PAGE_REQUEST_TYPE
    {
        IMAPI_MODE_PAGE_REQUEST_TYPE_CURRENT_VALUES = 0,
        IMAPI_MODE_PAGE_REQUEST_TYPE_CHANGABLE_VALUES = 1,
        IMAPI_MODE_PAGE_REQUEST_TYPE_DEFAULT_VALUES = 2,
        IMAPI_MODE_PAGE_REQUEST_TYPE_SAVED_VALUES = 3,
    }
}