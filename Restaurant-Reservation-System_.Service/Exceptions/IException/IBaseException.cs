

using System.Net;

namespace Restaurant_Reservation_System_.Service.Exceptions.IExceptions
{
    public interface IBaseException
    {
        public HttpStatusCode StatusCode { get; set; }
    }
}
