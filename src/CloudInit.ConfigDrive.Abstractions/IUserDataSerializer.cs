using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Dbosoft.CloudInit.ConfigDrive
{
    public interface IUserDataSerializer
    {
        Task<Stream> SerializeUserData(IEnumerable<UserData> userData, UserDataOptions options);
    }
}