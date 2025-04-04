using Restaurant_Reservation_System_.Core.Entittes.Comman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Reservation_System_.Core.Entittes
{
    public class Subscribe:BaseEntity
    {
        public string Email { get; set; } = null!;
        public bool IsSubscribed { get; set; } = false;
    }
}
