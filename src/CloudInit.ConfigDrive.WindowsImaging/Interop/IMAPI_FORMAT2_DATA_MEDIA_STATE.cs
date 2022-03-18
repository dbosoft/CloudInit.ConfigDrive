// copyright dbosoft GmbH / licensed under MIT license
// based on sample code by Microsoft Windows SDK samples 
// https://github.com/microsoft/Windows-classic-samples/blob/master/Samples/Win7Samples/winbase/imapi/interop/IMAPIv2.cs


// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global

using System.Runtime.InteropServices;

namespace Dbosoft.CloudInit.ConfigDrive.Interop
{
    public enum IMAPI_FORMAT2_DATA_MEDIA_STATE
    {
        [TypeLibVar(TypeLibVarFlags.FHidden)]
        IMAPI_FORMAT2_DATA_MEDIA_STATE_UNKNOWN = 0,
        IMAPI_FORMAT2_DATA_MEDIA_STATE_OVERWRITE_ONLY = 1,
        IMAPI_FORMAT2_DATA_MEDIA_STATE_RANDOMLY_WRITABLE = 1,
        IMAPI_FORMAT2_DATA_MEDIA_STATE_BLANK = 2,
        IMAPI_FORMAT2_DATA_MEDIA_STATE_APPENDABLE = 4,
        IMAPI_FORMAT2_DATA_MEDIA_STATE_FINAL_SESSION = 8,
        IMAPI_FORMAT2_DATA_MEDIA_STATE_INFORMATIONAL_MASK = 15,
        IMAPI_FORMAT2_DATA_MEDIA_STATE_DAMAGED = 1024,
        IMAPI_FORMAT2_DATA_MEDIA_STATE_ERASE_REQUIRED = 2048,
        IMAPI_FORMAT2_DATA_MEDIA_STATE_NON_EMPTY_SESSION = 4096,
        IMAPI_FORMAT2_DATA_MEDIA_STATE_WRITE_PROTECTED = 8192,
        IMAPI_FORMAT2_DATA_MEDIA_STATE_FINALIZED = 16384,
        IMAPI_FORMAT2_DATA_MEDIA_STATE_UNSUPPORTED_MEDIA = 32768,
        IMAPI_FORMAT2_DATA_MEDIA_STATE_UNSUPPORTED_MASK = 64512,
    }
}