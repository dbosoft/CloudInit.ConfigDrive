using System;
using System.Runtime.Serialization;

[Serializable]
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

    protected CloudInitConfigurationException(
        SerializationInfo info,
        StreamingContext context) : base(info, context)
    {
    }
}