
using Restaurant_Reservation_System_.Service.Exceptions.IExceptions;
using System.Net;

namespace Restaurant_Reservation_System_.Service.Exceptions
{
    public class InvalidInputException : Exception, IBaseException
    {
        public InvalidInputException(string message) : base(message)
        {

        }

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;

    }
}
