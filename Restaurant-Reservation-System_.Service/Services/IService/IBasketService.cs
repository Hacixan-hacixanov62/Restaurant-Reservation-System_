using Restaurant_Reservation_System_.Service.Dtos.BasketDtos;

namespace Restaurant_Reservation_System_.Service.Services.IService
{
    public interface IBasketService
    {
        Task<bool> AddToCartAsync(int id, int count = 1);
        Task<bool> DecreaseToCartAsync(int id);
        Task RemoveToCartAsync(int id);
        Task ClearCartAsync();

        Task RemoveToBasket(int id, string? returnUrl);
        Task EditBasketItem(int id, int count);
        Task AddToBasket(int id, string? returnUrl, int count = 1, int page = 1);
        CartGetDto GetUserBasketItem();
    }
}
