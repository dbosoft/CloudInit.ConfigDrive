// copyright: rebus-org https://github.com/rebus-org
// source: https://raw.githubusercontent.com/rebus-org/Rebus/master/Rebus/Injection/IResolutionContext.cs

using System.Collections;

namespace Dbosoft.CloudInit.ConfigDrive.Injection
{
    /// <summary>
    /// Represents the context of resolving one root service and can be used throughout the tree to fetch something to be injected
    /// </summary>
    public interface IResolutionContext
    {
        /// <summary>
        /// Gets an instance of the specified <typeparamref name="TService"/>.
        /// </summary>
        TService Get<TService>();

        /// <summary>
        /// Gets all instances resolved within this resolution context at this time.
        /// </summary>
        IEnumerable TrackedInstances { get; }

        /// <summary>
        /// Gets whether there exists a primary registration for the <typeparamref name="TService"/> type
        /// </summary>
        bool Has<TService>(bool primary = true);
    }
}