using Restaurant_Reservation_System_.Core.Entittes.Comman;

namespace Restaurant_Reservation_System_.Core.Entittes
{
    public class About :BaseEntity
    {
        public string Title  { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string ButtonText { get; set; }
        public string ButtonLink { get; set; }
        public string Image { get; set; }
    }
}
