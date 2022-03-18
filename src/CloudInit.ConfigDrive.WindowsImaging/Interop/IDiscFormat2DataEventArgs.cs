// copyright dbosoft GmbH / licensed under MIT license
// based on sample code by Microsoft Windows SDK samples 
// https://github.com/microsoft/Windows-classic-samples/blob/master/Samples/Win7Samples/winbase/imapi/interop/IMAPIv2.cs


using System.Runtime.InteropServices;

// ReSharper disable UnusedMember.Global

namespace Dbosoft.CloudInit.ConfigDrive.Interop
{
    [TypeLibType(TypeLibTypeFlags.FDual |
                 TypeLibTypeFlags.FDispatchable |
                 TypeLibTypeFlags.FNonExtensible)]
    [Guid("2735413D-7F64-5B0F-8F00-5D77AFBE261E")]
    public interface IDiscFormat2DataEventArgs
    {
        // IWriteEngine2EventArgs
        /// <summary>
        /// The starting logical block address for the current write operation.
        /// </summary>
        [DispId(256)]
        int StartLba { get; }

        /// <summary>
        /// The number of sectors being written for the current write operation.
        /// </summary>
        [DispId(257)]
        int SectorCount { get; }

        /// <summary>
        /// The last logical block address of data read for the current write operation.
        /// </summary>
        [DispId(258)]
        int LastReadLba { get; }

        /// <summary>
        /// The last logical block address of data written for the current write operation
        /// </summary>
        [DispId(259)]
        int LastWrittenLba { get; }

        /// <summary>
        /// The total bytes available in the system's cache buffer
        /// </summary>
        [DispId(262)]
        int TotalSystemBuffer { get; }

        /// <summary>
        /// The used bytes in the system's cache buffer
        /// </summary>
        [DispId(263)]
        int UsedSystemBuffer { get; }

        /// <summary>
        /// The free bytes in the system's cache buffer
        /// </summary>
        [DispId(264)]
        int FreeSystemBuffer { get; }

        // IDiscFormat2DataEventArgs
        /// <summary>
        /// The total elapsed time for the current write operation.
        /// </summary>
        [DispId(768)]
        int ElapsedTime { get; }

        /// <summary>
        /// The estimated time remaining for the write operation.
        /// </summary>
        [DispId(769)]
        int RemainingTime { get; }

        /// <summary>
        /// The estimated total time for the write operation.
        /// </summary>
        [DispId(770)]
        int TotalTime { get; }

        /// <summary>
        /// The current write action.
        /// </summary>
        [DispId(771)]
        IMAPI_FORMAT2_DATA_WRITE_ACTION CurrentAction { get; }
    }
}