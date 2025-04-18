using Restaurant_Reservation_System_.Core.Entittes.Comman;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Reservation_System_.Core.Entittes
{
    public class Table:BaseEntity
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Table duzgun daxil edin.")]
        public int TableNo { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Şəxslərin sayı 1 ilə 10 arasında olmalıdır.")]
        public int PersonCount { get; set; }
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    }
}
