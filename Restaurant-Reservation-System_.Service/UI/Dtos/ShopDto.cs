
using Restaurant_Reservation_System_.Service.Dtos.CategoryDtos;
using Restaurant_Reservation_System_.Service.Dtos.ProductDtos;

namespace Restaurant_Reservation_System_.Service.UI.Dtos
{
    public class ShopDto
    {
        public List<ProductGetDto> Products { get; set; } = new();
        public List<CategoryGetDto> Categories { get; set; } = new();
    }
}
