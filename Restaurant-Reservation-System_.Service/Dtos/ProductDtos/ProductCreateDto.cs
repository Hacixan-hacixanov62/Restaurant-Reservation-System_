using Microsoft.AspNetCore.Http;
using Restaurant_Reservation_System_.Service.Dtos.CategoryDtos;
using Restaurant_Reservation_System_.Service.Dtos.ProductDetailDtos;

namespace Restaurant_Reservation_System_.Service.Dtos.ProductDtos
{
    public class ProductCreateDto
    {
        public List<CategoryGetDto>? Categories { get; set; } = [];
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public IFormFile MainImage { get; set; } = null!;
        public List<IFormFile> Images { get; set; } = [];
        public List<ProductDetailCreateDto> ProductDetails { get; set; } = [];
    }
}
