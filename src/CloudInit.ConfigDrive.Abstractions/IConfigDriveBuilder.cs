using System;
using Dbosoft.CloudInit.ConfigDrive.Injection;

namespace Dbosoft.CloudInit.ConfigDrive
{
    public interface IConfigDriveBuilder
    {
        IConfigDriveBuilder With<T>(T instance) where T : class;
        IConfigDriveBuilder With<T>(Func<IResolutionContext, T> resolverMethod) where T : class;
        IConfigDrive Build();
    }
}