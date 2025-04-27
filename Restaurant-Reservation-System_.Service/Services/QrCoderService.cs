using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Restaurant_Reservation_System_.Service.Services.IService;
using Microsoft.AspNetCore.Hosting;

namespace Restaurant_Reservation_System_.Service.Services
{
    public class QrCoderService : IQrCoderService
    {
        private readonly IWebHostEnvironment _env;

        public QrCoderService(IWebHostEnvironment env)
        {
            _env = env;
        }

        // Demeli asagdaki Method QR linkini yaradir
        public (string imageUrl, string bookingId) GenerateQRCode()
        {
            string bookingId = Guid.NewGuid().ToString();

            string qrContent = $"http://172.16.1.243:5159/fakeMenuLink/{bookingId}";

            QRCodeGenerator qrCodeGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrCodeGenerator.CreateQrCode(qrContent, QRCodeGenerator.ECCLevel.Q);
            BitmapByteQRCode qrCode = new BitmapByteQRCode(qrCodeData);
            byte[] qrCodeBytes = qrCode.GetGraphic(20);

            string fileName = $"qr_{bookingId}.png";
            string relativeFolder = "assets/images/qr"; 
            string folderPath = Path.Combine(_env.WebRootPath, relativeFolder);
            string filePath = Path.Combine(folderPath, fileName);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            System.IO.File.WriteAllBytes(filePath, qrCodeBytes);

            string imageUrl = $"/{relativeFolder}/{fileName}";

            return (imageUrl, bookingId);
        }

        public string GenerateQrCodeForUrl(string url)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);

            var qrCode = new BitmapByteQRCode(qrCodeData);
            byte[] qrCodeBytes = qrCode.GetGraphic(20);

            using (MemoryStream ms = new MemoryStream(qrCodeBytes))
            {
                using (Bitmap bmp = new Bitmap(ms))
                {
                    using (MemoryStream pngStream = new MemoryStream())
                    {
                        bmp.Save(pngStream, ImageFormat.Png);
                        pngStream.Position = 0;
                        return $"data:image/png;base64,{Convert.ToBase64String(pngStream.ToArray())}";
                    }
                }
            }
        }

    }
}


