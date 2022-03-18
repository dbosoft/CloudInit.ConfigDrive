// copyright dbosoft GmbH / licensed under MIT license
// based on sample code by Microsoft Windows SDK samples 
// https://github.com/microsoft/Windows-classic-samples/blob/master/Samples/Win7Samples/winbase/imapi/interop/IMAPIv2.cs


using System.Collections;
using System.Runtime.InteropServices;

namespace Dbosoft.CloudInit.ConfigDrive.Interop
{
    [TypeLibType(TypeLibTypeFlags.FDual |
                 TypeLibTypeFlags.FDispatchable |
                 TypeLibTypeFlags.FNonExtensible)]
    [Guid("27354130-7F64-5B0F-8F00-5D77AFBE261E"), ComImport]
    public interface IDiscMaster2
    {
        /// <summary>
        /// Enumerates the list of CD/DVD devices on the system
        /// </summary>
        /// <returns></returns>
        [TypeLibFunc(65)]
        [DispId(-4)]
        IEnumerator GetEnumerator();

        /// <summary>
        /// Gets a single recorder's ID
        /// </summary>
        /// <param name="index">Zero based index</param>
        /// <returns>Recorder's unique id</returns>
        [DispId(0)]
        string this[int index] { [return: MarshalAs(UnmanagedType.BStr)] get; }

        /// <summary>
        /// The current number of recorders in the system.
        /// </summary>
        [DispId(1)]
        int Count { get; }

        /// <summary>
        /// Whether IMAPI is running in an environment with optical 
        /// devices and permission to access them.
        /// </summary>
        [DispId(2)]
        bool IsSupportedEnvironment { [return: MarshalAs(UnmanagedType.VariantBool)] get; }
    }
}