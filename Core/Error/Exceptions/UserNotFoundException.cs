using System;
using System.Net;

namespace ShopsRUs.Service.Core.Error.Exceptions
{
    public class UserNotFoundException : ApiException
    {
        public UserNotFoundException() : this("User Not Found")
        {
        }


        public UserNotFoundException(string message) : base(message, HttpStatusCode.NotFound)
        {

        }

        public UserNotFoundException(string message, Exception innerException) : base(message, innerException, HttpStatusCode.NotFound)
        {

        }
    }
}