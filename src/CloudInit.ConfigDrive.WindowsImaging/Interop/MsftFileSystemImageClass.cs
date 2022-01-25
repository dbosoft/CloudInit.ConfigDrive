// copyright dbosoft GmbH / licensed under MIT license
// based on sample code by Microsoft Windows SDK samples 
// https://github.com/microsoft/Windows-classic-samples/blob/master/Samples/Win7Samples/winbase/imapi/interop/IMAPIv2.cs


using System.Runtime.InteropServices;

namespace Dbosoft.CloudInit.ConfigDrive.Interop
{
    [TypeLibType(TypeLibTypeFlags.FCanCreate)]
    [ClassInterface(ClassInterfaceType.None)]
#pragma warning disable 618
    [ComSourceInterfaces("DFileSystemImageEvents\0")]
#pragma warning restore 618
    [Guid("2C941FC5-975B-59BE-A960-9A2A262853A5"), ComImport]
    public class MsftFileSystemImageClass
    {
    }
}