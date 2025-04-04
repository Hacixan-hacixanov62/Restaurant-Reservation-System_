
using Restaurant_Reservation_System_.Service.Abstractions.Dtos;

namespace Restaurant_Reservation_System_.Service.Dtos.CommanDtos
{
    public class ErrorDto:IDto
    {
        public string Name { get; set; } = "Xəta baş verdi";
        public string Message { get; set; } = null!;
        public int StatusCode { get; set; }
    }
}
