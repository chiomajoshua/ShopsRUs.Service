using System;
using System.Net;

namespace ShopsRUs.Service.Core.Error.Exceptions
{
    public class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public ApiException() : base("Internal Api Exception")
        {
            StatusCode = HttpStatusCode.InternalServerError;
        }

        public ApiException(string message, HttpStatusCode statusCode) : base(message)
        {
            if (statusCode == default) statusCode = HttpStatusCode.InternalServerError;
            StatusCode = statusCode;
        }

        public ApiException(string message, Exception innerException, HttpStatusCode statusCode) : base(message,
            innerException)
        {

        }
    }
}