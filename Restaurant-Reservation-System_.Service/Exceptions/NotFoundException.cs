
using Restaurant_Reservation_System_.Service.Exceptions.IExceptions;
using System.Net;

namespace Restaurant_Reservation_System_.Service.Exceptions
{
    public class NotFoundException : Exception, IBaseException
    {
        public NotFoundException(string message = "Not found") : base(message)
        {

        }

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.NotFound;

    }
}
