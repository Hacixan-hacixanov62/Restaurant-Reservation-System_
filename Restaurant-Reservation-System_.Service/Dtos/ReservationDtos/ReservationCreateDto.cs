using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Reservation_System_.Service.Dtos.ReservationDtos
{
    public class ReservationCreateDto
    {
        [Required(ErrorMessage = "TableNo is required")]
        public int TableId { get; set; }

        [Required(ErrorMessage = "Time is required")]
        public DateTime Time { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email adress")]
        public string Email { get; set; } = null!;
    }
}
