using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Restaurant_Reservation_System_.Service.Services.IService;

namespace Restaurant_Reservation_System_.Service.Services
{
    public class QrCoderService : IQrCoderService
    {
        public string GenerateQrCode(string content)
        {
            using (var qrGenerator = new QRCodeGenerator())
            {
                var qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);

                // BitmapByteQRCode istifade olunur (QRCode class-i yerine)
                var qrCode = new BitmapByteQRCode(qrCodeData);
                byte[] qrCodeBytes = qrCode.GetGraphic(20);

                // Base64 string-ə çeviririk
                string base64String = Convert.ToBase64String(qrCodeBytes);
                return $"data:image/png;base64,{base64String}";
            }
        }
    }
}


