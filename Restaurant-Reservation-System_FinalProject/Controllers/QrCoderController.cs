using Microsoft.AspNetCore.Mvc;
using Restaurant_Reservation_System_.Service.Services.IService;

namespace Restaurant_Reservation_System_FinalProject.Controllers
{
    public class QrCoderController : Controller
    {
        private readonly IQrCoderService _qrCoderService;

        public QrCoderController(IQrCoderService qrCoderService)
        {
            _qrCoderService = qrCoderService;
        }

        [HttpPost]
        public IActionResult Index(string value)
        {
            string base64QrImage = _qrCoderService.GenerateQrCode(value);
            ViewBag.QRCodeImage = base64QrImage;
            return View();
        }


    }
}
