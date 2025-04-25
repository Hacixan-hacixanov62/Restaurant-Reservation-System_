
namespace Restaurant_Reservation_System_.Service.Services.IService
{
    public interface IQrCoderService
    {
        (string imageUrl, string bookingId) GenerateQRCode();
        string GenerateQrCodeForUrl(string url);
    }
}
