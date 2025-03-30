using Microsoft.AspNetCore.Http;
using Restaurant_Reservation_System_.Service.Abstractions.Dtos;
using Restaurant_Reservation_System_.Service.Dtos.CategoryDtos;
using Restaurant_Reservation_System_.Service.Dtos.ProductDetailDtos;


namespace Restaurant_Reservation_System_.Service.Dtos.ProductDtos
{
    public class ProductUpdateDto:IDto
    {
        public int Id { get; set; }
        public List<CategoryGetDto>? Categories { get; set; } = [];
        public int CategoryId { get; set; }

        public string? MainImagePath { get; set; }
        public IFormFile? MainImage { get; set; }
        public List<IFormFile> Images { get; set; } = [];
        public List<string> ImagePaths { get; set; } = [];
        public List<int> ImageIds { get; set; } = [];

        public decimal Price { get; set; }
        public List<ProductDetailUpdateDto> ProductDetails { get; set; } = [];
    }
}
