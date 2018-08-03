using System.Runtime.InteropServices;

namespace Contiva.Windows.ImagingApi.Interop
{
    [CoClass(typeof(MsftDiscFormat2DataClass)), ComImport]
    [Guid("27354153-9F64-5B0F-8F00-5D77AFBE261E")]
    public interface MsftDiscFormat2Data : IDiscFormat2Data, DiscFormat2Data_Events
    {
    }
}