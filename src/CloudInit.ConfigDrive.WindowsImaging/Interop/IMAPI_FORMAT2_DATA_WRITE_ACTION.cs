// copyright dbosoft GmbH / licensed under MIT license
// based on sample code by Microsoft Windows SDK samples 
// https://github.com/microsoft/Windows-classic-samples/blob/master/Samples/Win7Samples/winbase/imapi/interop/IMAPIv2.cs


// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global

namespace Dbosoft.CloudInit.ConfigDrive.Interop
{
    public enum IMAPI_FORMAT2_DATA_WRITE_ACTION
    {
        IMAPI_FORMAT2_DATA_WRITE_ACTION_VALIDATING_MEDIA = 0,
        IMAPI_FORMAT2_DATA_WRITE_ACTION_FORMATTING_MEDIA = 1,
        IMAPI_FORMAT2_DATA_WRITE_ACTION_INITIALIZING_HARDWARE = 2,
        IMAPI_FORMAT2_DATA_WRITE_ACTION_CALIBRATING_POWER = 3,
        IMAPI_FORMAT2_DATA_WRITE_ACTION_WRITING_DATA = 4,
        IMAPI_FORMAT2_DATA_WRITE_ACTION_FINALIZATION = 5,
        IMAPI_FORMAT2_DATA_WRITE_ACTION_COMPLETED = 6,
    }
}