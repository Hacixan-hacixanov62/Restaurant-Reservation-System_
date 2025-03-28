using Microsoft.AspNetCore.Http;
using Restaurant_Reservation_System_.Service.ViewModels.CategoryVM;
using Restaurant_Reservation_System_.Service.ViewModels.ProductDetailVM;

namespace Restaurant_Reservation_System_.Service.ViewModels.ProductVM
{
    public class ProductUpdateVM 
    {
        public int Id { get; set; }
        public List<CategoryGetVM>? Categories { get; set; } = [];
        public int CategoryId { get; set; }

        public string? MainImagePath { get; set; }
        public IFormFile? MainImage { get; set; }
        public List<IFormFile> Images { get; set; } = [];
        public List<string> ImagePaths { get; set; } = [];
        public List<int> ImageIds { get; set; } = [];

        public decimal Price { get; set; }
        public List<ProductDetailUpdateVM> ProductDetails { get; set; } = [];
    }
}
 