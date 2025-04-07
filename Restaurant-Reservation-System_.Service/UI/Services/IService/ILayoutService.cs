
using Restaurant_Reservation_System_.Service.Dtos.BasketDtos;

namespace Restaurant_Reservation_System_.Service.UI.Services.IService
{
    public interface ILayoutService
    {
        Task<CartGetDto> GetCartAsync();
    }
}
