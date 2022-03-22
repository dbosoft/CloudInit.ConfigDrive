using System.Threading.Tasks;

namespace Dbosoft.CloudInit.ConfigDrive
{
    public interface IConfigDriveWriter
    {
        Task WriteConfigDrive(IConfigDrive configDrive);
    }
}