// copyright: rebus-org https://github.com/rebus-org
// source: https://raw.githubusercontent.com/rebus-org/Rebus/master/Rebus/Injection/ResolutionResult.cs

using System;
using System.Collections;

namespace Dbosoft.CloudInit.ConfigDrive.Injection
{
    /// <summary>
    /// Contains a built object instance along with all the objects that were used to build the instance
    /// </summary>
    public class ResolutionResult<TService>
    {
        internal ResolutionResult(TService instance)
        {
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            Instance = instance;
        }

        /// <summary>
        /// Gets the instance that was built
        /// </summary>
        public TService Instance { get; }

    }
}