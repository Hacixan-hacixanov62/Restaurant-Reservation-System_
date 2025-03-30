
using Restaurant_Reservation_System_.Service.Abstractions.Dtos;
using Restaurant_Reservation_System_.Service.Dtos.CategoryDtos;

namespace Restaurant_Reservation_System_.Service.Dtos.ProductDtos
{
    public class ProductGetDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public List<CategoryGetDto> Categories { get; set; } = null!;
        public string MainImagePath { get; set; } = null!;
        public List<string> ImagePaths { get; set; } = [];
        public int CategoryId { get; set; } 


        public List<ProductGetDto> Products { get; set; } = [];


    }
}
