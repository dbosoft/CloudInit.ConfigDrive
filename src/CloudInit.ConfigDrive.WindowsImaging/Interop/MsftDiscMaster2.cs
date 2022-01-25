// copyright dbosoft GmbH / licensed under MIT license
// based on sample code by Microsoft Windows SDK samples 
// https://github.com/microsoft/Windows-classic-samples/blob/master/Samples/Win7Samples/winbase/imapi/interop/IMAPIv2.cs


using System.Runtime.InteropServices;

namespace Dbosoft.CloudInit.ConfigDrive.Interop
{
    [CoClass(typeof(MsftDiscMaster2Class)), ComImport]
    [Guid("27354130-7F64-5B0F-8F00-5D77AFBE261E")]
    public interface MsftDiscMaster2 : IDiscMaster2
    {
    }
}