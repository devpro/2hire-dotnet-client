using System;
using System.Runtime.Serialization;

namespace Devpro.Twohire.Abstractions.Exceptions
{
    [Serializable]
    public class ConnectivityException : Exception
    {
        public ConnectivityException()
        {
        }

        public ConnectivityException(string message)
            : base(message)
        {
        }

        public ConnectivityException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ConnectivityException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}
