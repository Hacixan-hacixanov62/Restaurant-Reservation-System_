using Microsoft.AspNetCore.Http;
using Restaurant_Reservation_System_.Service.ViewModels.CategoryVM;

namespace Restaurant_Reservation_System_.Service.ViewModels.ProductVM
{
    public class ProductCreateVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IFormFile MainImage { get; set; } = null!;
        public int CategoryId { get; set; }
    }
}
