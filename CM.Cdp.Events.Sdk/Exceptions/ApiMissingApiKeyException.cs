using System;
using System.Runtime.Serialization;

namespace CM.Cdp.Events.Sdk.Exceptions
{
    public class ApiMissingApiKeyException : Exception
    {
        public ApiMissingApiKeyException() { }

        public ApiMissingApiKeyException(string message) : base(message) { }

        public ApiMissingApiKeyException(string message, Exception innerException) : base(message, innerException) { }

        protected ApiMissingApiKeyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }       
    }
}
