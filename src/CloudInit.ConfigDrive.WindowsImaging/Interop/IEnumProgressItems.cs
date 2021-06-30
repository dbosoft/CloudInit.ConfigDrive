// copyright dbosoft GmbH / licensed under MIT license
// based on sample code by Microsoft Windows SDK samples 
// https://github.com/microsoft/Windows-classic-samples/blob/master/Samples/Win7Samples/winbase/imapi/interop/IMAPIv2.cs


using System.Runtime.InteropServices;

// ReSharper disable UnusedMember.Global

namespace Dbosoft.CloudInit.ConfigDrive.Interop
{
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("2C941FD6-975B-59BE-A960-9A2A262853A5")]
    public interface IEnumProgressItems
    {
        /// <summary>
        /// Get next items in the enumeration
        /// </summary>
        /// <param name="celt"></param>
        /// <param name="rgelt"></param>
        /// <param name="pceltFetched"></param>
        void Next(uint celt, out IProgressItem rgelt, out uint pceltFetched);

        /// <summary>
        /// Remoting support for Next (allow NULL pointer for item count when 
        /// requesting single item)
        /// </summary>
        /// <param name="celt"></param>
        /// <param name="rgelt"></param>
        /// <param name="pceltFetched"></param>
        void RemoteNext(uint celt, out IProgressItem rgelt, out uint pceltFetched);

        /// <summary>
        /// Skip items in the enumeration
        /// </summary>
        /// <param name="celt"></param>
        void Skip(uint celt);

        /// <summary>
        /// Reset the enumerator
        /// </summary>
        void Reset();

        /// <summary>
        /// Make a copy of the enumerator
        /// </summary>
        /// <param name="ppEnum"></param>
        void Clone(out IEnumProgressItems ppEnum);
    }
}