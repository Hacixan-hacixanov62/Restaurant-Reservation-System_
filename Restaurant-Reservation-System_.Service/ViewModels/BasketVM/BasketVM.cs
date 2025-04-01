namespace Restaurant_Reservation_System_.Service.ViewModels.BasketVM
{
    public class BasketVM
    {
        public List<BasketItemVM> Items { get; set; } = new List<BasketItemVM>();

        public decimal TotalAmount { get; set; }
    }
}
