// copyright dbosoft GmbH / licensed under MIT license
// based on sample code by Microsoft Windows SDK samples 
// https://github.com/microsoft/Windows-classic-samples/blob/master/Samples/Win7Samples/winbase/imapi/interop/IMAPIv2.cs


// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global

namespace Dbosoft.CloudInit.ConfigDrive.Interop
{
    public enum IMAPI_MODE_PAGE_TYPE
    {
        IMAPI_MODE_PAGE_TYPE_READ_WRITE_ERROR_RECOVERY = 1,
        IMAPI_MODE_PAGE_TYPE_MRW = 3,
        IMAPI_MODE_PAGE_TYPE_WRITE_PARAMETERS = 5,
        IMAPI_MODE_PAGE_TYPE_CACHING = 8,
        IMAPI_MODE_PAGE_TYPE_POWER_CONDITION = 26,
        IMAPI_MODE_PAGE_TYPE_INFORMATIONAL_EXCEPTIONS = 28,
        IMAPI_MODE_PAGE_TYPE_TIMEOUT_AND_PROTECT = 29,
        IMAPI_MODE_PAGE_TYPE_LEGACY_CAPABILITIES = 42,
    }
}