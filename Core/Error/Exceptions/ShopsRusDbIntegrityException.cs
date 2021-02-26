using System.Net;

namespace ShopsRUs.Service.Core.Error.Exceptions
{
    public class ShopsRusDbIntegrityException : ApiException
    {
        public ShopsRusDbIntegrityException(string message) : base(message, HttpStatusCode.BadRequest)
        {

        }
    }
}
