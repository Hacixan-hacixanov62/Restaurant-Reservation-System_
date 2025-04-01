namespace Restaurant_Reservation_System_.Service.Services.IService
{
    public interface IBasketService
    {
        Task<bool> AddToCartAsync(int id, int count = 1);
        Task<bool> DecreaseToCartAsync(int id);
        Task RemoveToCartAsync(int id);
        Task ClearCartAsync();
    }
}
