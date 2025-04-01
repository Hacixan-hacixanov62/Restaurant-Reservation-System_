using Restaurant_Reservation_System_.Core.Entittes;

namespace Restaurant_Reservation_System_.Service.ViewModels.BasketVM
{
    public class BasketItemVM
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public string Name { get; set; }
        public string MainImage { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
    }
}
