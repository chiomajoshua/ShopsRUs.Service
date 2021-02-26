using System.Net;

namespace ShopsRUs.Service.Core.Error.Exceptions
{
    public class AuthException : ApiException
    {
        public AuthException(string message) : base(message, HttpStatusCode.Unauthorized)
        {

        }
    }

    public class UserNotAuthenticatedException : AuthException
    {
        public UserNotAuthenticatedException() : base("User authentication failed")
        {

        }
    }

    public class UserNotAuthorizedException : AuthException
    {
        public UserNotAuthorizedException() : base("User is not authorized")
        {

        }
    }

    public class UserIsBlockedException : AuthException
    {
        public UserIsBlockedException() : base("User is blocked")
        {

        }
    }
}