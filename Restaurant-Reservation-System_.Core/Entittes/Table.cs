using Restaurant_Reservation_System_.Core.Entittes.Comman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Reservation_System_.Core.Entittes
{
    public class Table:BaseEntity
    {
        public int TableNo { get; set; }
        public int PersonCount { get; set; }
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    }
}
