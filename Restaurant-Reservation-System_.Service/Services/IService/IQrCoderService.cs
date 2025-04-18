
namespace Restaurant_Reservation_System_.Service.Services.IService
{
    public interface IQrCoderService
    {
        byte[] GenerateQrCode(string content);
    }
}
