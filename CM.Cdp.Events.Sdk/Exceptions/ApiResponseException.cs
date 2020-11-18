using System;
using System.Net;
using System.Runtime.Serialization;

namespace CM.Cdp.Events.Sdk.Exceptions
{
    public class ApiResponseException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public string Content { get; }

        public ApiResponseException()
        {
        }

        protected ApiResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ApiResponseException(HttpStatusCode statusCode, string content, string message) : base(message)
        {
            StatusCode = statusCode;
            Content = content;
        }
    }
}