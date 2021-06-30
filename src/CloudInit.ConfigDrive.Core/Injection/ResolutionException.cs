// copyright: rebus-org https://github.com/rebus-org
// source: https://raw.githubusercontent.com/rebus-org/Rebus/master/Rebus/Injection/ResolutionException.cs

#if NET45

#elif NETSTANDARD
using System.Runtime.Serialization;
#endif
using System;

namespace Dbosoft.CloudInit.ConfigDrive.Injection
{
    /// <summary>
    /// Exceptions that is thrown when something goes wrong while working with the injectionist
    /// </summary>
#if NET45
    [Serializable]
#elif NETSTANDARD2_0
    [Serializable]
#endif
    public class ResolutionException : Exception
    {
        /// <summary>
        /// Constructs the exception
        /// </summary>
        public ResolutionException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructs the exception
        /// </summary>
        public ResolutionException(Exception innerException, string message)
            : base(message, innerException)
        {
        }

#if NET45
/// <summary>
/// Constructs the exception
/// </summary>
        public ResolutionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#elif NETSTANDARD2_0
        /// <summary>
        /// Constructs the exception
        /// </summary>
        public ResolutionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}