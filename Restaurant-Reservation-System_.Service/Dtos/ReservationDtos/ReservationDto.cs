using Restaurant_Reservation_System_.Service.Abstractions.Dtos;
using Restaurant_Reservation_System_.Service.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Reservation_System_.Service.Dtos.ReservationDtos
{
    public class ReservationDto:IDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "ReservationNumber duzgun daxil edin.")]
        public string ReservationNumber { get; set; } = null!;
        [Required]
        [StringLength(maximumLength: 150)]
        [RegularExpression(@"^[^\d]*$", ErrorMessage = "Name Duzgun daxil edin.")]
        public string Name { get; set; } = null!;
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Time is required")]
        public string Time { get; set; } = null!;
        public ICollection<ProductGetDto> Products { get; set; } = [];
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "NumberOfPeople duzgun daxil edin.")]
        public int NumberOfPeople { get; set; }
    }
}
