
using Restaurant_Reservation_System_.Service.Dtos.CommentDtos;
using Restaurant_Reservation_System_.Service.Dtos.ProductDtos;

namespace Restaurant_Reservation_System_.Service.UI.Dtos
{
    public class ShopDetailDto
    {
        public ProductGetDto Product { get; set; } = null!;
        public List<CommentGetDto> Comments { get; set; } = [];
        public bool IsAllowComment { get; set; } = false;
    }
}
