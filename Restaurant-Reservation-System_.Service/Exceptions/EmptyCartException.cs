
using Restaurant_Reservation_System_.Service.Exceptions.IExceptions;
using System.Net;

namespace Restaurant_Reservation_System_.Service.Exceptions
{
    public class EmptyCartException : Exception, IBaseException
    {
        public EmptyCartException(string message = "Səbətiniz boşdur") : base(message)
        {

        }
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.NotFound;

    }
}
