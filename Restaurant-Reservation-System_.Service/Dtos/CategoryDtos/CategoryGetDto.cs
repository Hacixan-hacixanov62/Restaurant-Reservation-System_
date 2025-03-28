

using Restaurant_Reservation_System_.Service.Dtos.ProductDtos;

namespace Restaurant_Reservation_System_.Service.Dtos.CategoryDtos
{
    public class CategoryGetDto
    {
        public int Id { get; set; }
       
        public string Name { get; set; } = null!;
        
        public List<ProductGetDto> Products { get; set; } = [];
    }
}
