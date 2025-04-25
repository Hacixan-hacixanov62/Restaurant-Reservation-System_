using Microsoft.AspNetCore.Mvc;
using QRCoder;
using Restaurant_Reservation_System_.Service.Dtos.ReservationDtos;
using Restaurant_Reservation_System_.Service.Services;
using Restaurant_Reservation_System_.Service.Services.IService;
using System.Drawing;
using System.Drawing.Imaging;

namespace Restaurant_Reservation_System_FinalProject.Controllers
{
    public class QrCoderController : Controller
    {
        private readonly IQrCoderService _qrCoderService;
        private readonly IEmailService _emailService;

        public QrCoderController(IQrCoderService qrCoderService, IEmailService emailService)
        {
            _qrCoderService = qrCoderService;
            _emailService = emailService;
        }
        public IActionResult Restaurant()
        {
            var (qrCodeImage, bookingId) =  _qrCoderService.GenerateQRCode();
            ViewBag.QRCodeImage = qrCodeImage;
            ViewBag.BookingId = bookingId;

            return View();
        }

    }
}
