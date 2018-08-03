using System.Collections;
using System.Runtime.InteropServices;

namespace Contiva.Windows.ImagingApi.Interop
{
    [TypeLibType(TypeLibTypeFlags.FDual |
                 TypeLibTypeFlags.FDispatchable |
                 TypeLibTypeFlags.FNonExtensible)]
    [Guid("2C941FD7-975B-59BE-A960-9A2A262853A5")]
    public interface IProgressItems
    {
        /// <summary>
        /// Get an enumerator for the collection
        /// </summary>
        /// <returns></returns>
        [DispId(-4)]
        [TypeLibFunc(65)]
        IEnumerator GetEnumerator();

        /// <summary>
        /// Find the block mapping from the specified index
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        [DispId(0)]
        IProgressItem this[int Index] { get; }

        /// <summary>
        /// Number of items in the collection
        /// </summary>
        [DispId(1)]
        int Count { get; }

        /// <summary>
        /// Find the block mapping from the specified block
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        [DispId(2)]
        IProgressItem ProgressItemFromBlock(uint block);

        /// <summary>
        /// Find the block mapping from the specified item description
        /// </summary>
        /// <param name="Description"></param>
        /// <returns></returns>
        [DispId(3)]
        IProgressItem ProgressItemFromDescription(string Description);

        /// <summary>
        /// Get a non-variant enumerator
        /// </summary>
        [DispId(4)]
        IEnumProgressItems EnumProgressItems { get; }
    }
}