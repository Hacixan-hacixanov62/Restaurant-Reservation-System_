using Restaurant_Reservation_System_.Service.Abstractions.Dtos;
using Restaurant_Reservation_System_.Service.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Reservation_System_.Service.Dtos.ReservationDtos
{
    public class ReservationDto:IDto
    {
        public string ReservationNumber { get; set; } = null!;
        public string Name { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Time { get; set; } = null!;
        public ICollection<ProductGetDto> Products { get; set; } = [];
        public int NumberOfPeople { get; set; }
    }
}
