using Restaurant_Reservation_System_.Core.Entittes.Comman;
using System.ComponentModel.DataAnnotations;

namespace Restaurant_Reservation_System_.Core.Entittes
{
    public class Slider:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
