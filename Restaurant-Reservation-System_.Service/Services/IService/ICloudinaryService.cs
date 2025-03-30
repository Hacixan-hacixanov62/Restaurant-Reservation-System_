using Microsoft.AspNetCore.Http;


namespace Restaurant_Reservation_System_.Service.Services
{
    public interface ICloudinaryService
    {
        Task<string> FileCreateAsync(IFormFile file);
        Task<bool> FileDeleteAsync(string filePath);
    }
}
