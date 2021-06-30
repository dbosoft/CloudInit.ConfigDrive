// copyright dbosoft GmbH / licensed under MIT license
// based on sample code by Microsoft Windows SDK samples 
// https://github.com/microsoft/Windows-classic-samples/blob/master/Samples/Win7Samples/winbase/imapi/interop/IMAPIv2.cs


using System.Runtime.InteropServices;

namespace Dbosoft.CloudInit.ConfigDrive.Interop
{
    [TypeLibType(TypeLibTypeFlags.FCanCreate)]
    [ClassInterface(ClassInterfaceType.None)]
#pragma warning disable 618
    [ComSourceInterfaces("DDiscFormat2DataEvents\0")]
#pragma warning restore 618
    [Guid("2735412A-7F64-5B0F-8F00-5D77AFBE261E"), ComImport]
    public class MsftDiscFormat2DataClass
    {
    }
}