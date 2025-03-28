
using Restaurant_Reservation_System_.Service.Exceptions.IExceptions;
using System.Net;

namespace Restaurant_Reservation_System_.Service.Exceptions
{

    public class UnAuthorizedException : Exception, IBaseException
    {
        public UnAuthorizedException(string message = "Qeydiyyatdan keçməyən istifadəçi") : base(message)
        {

        }

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.Unauthorized;
    }
}
