
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Dbosoft.CloudInit.ConfigDrive
{

    [Serializable]
    [ExcludeFromCodeCoverage]
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

        public CloudInitConfigurationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

    }
}