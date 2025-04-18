
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Restaurant_Reservation_System_.Service.Services.IService;

namespace Restaurant_Reservation_System_.Service.Services
{
    public class QrCoderService : IQrCoderService
    {
        public byte[] GenerateQrCode(string content)
        {
            //using (var qrCodeGenerator = new QRCodeGenerator())
            //{
            //    var qrCodeData = qrCodeGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
            //    using (var qrCode = new QRCode(qrCodeData))
            //    {
            //        using (var bitmap = qrCode.GetGraphic(20))
            //        {
            //            using (var stream = new MemoryStream())
            //            {
            //                bitmap.Save(stream, ImageFormat.Png);
            //                return stream.ToArray();
            //            }
            //        }
            //    }
            //}
            throw new NotImplementedException();    
        }
    }
}
