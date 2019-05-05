using System;
using System.Runtime.Serialization;
#if NET45

#elif NETSTANDARD2_0
using System.Runtime.Serialization;
#endif

namespace Haipa.CloudInit.ConfigDrive
{
#if NET45
    [Serializable]
#elif NETSTANDARD2_0
    [Serializable]
#endif
    public class CloudInitConfigurationException : Exception
    {
        public CloudInitConfigurationException()
        {
        }

        public CloudInitConfigurationException(string message) : base(message)
        {
        }

        public CloudInitConfigurationException(string message, Exception inner) : base(message, inner)
        {
        }

#if NET45
/// <summary>
/// Constructs the exception
/// </summary>
        public CloudInitConfigurationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#elif NETSTANDARD2_0
/// <summary>
/// Constructs the exception
/// </summary>
        public CloudInitConfigurationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}