using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Host;
using Restaurant_Reservation_System_.Core.Enums;
using Restaurant_Reservation_System_.Service.Services.IService;
using Restaurant_Reservation_System_.Service.UI.Dtos;

namespace Restaurant_Reservation_System_FinalProject.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactDto dto)
        {
            var result = await _contactService.SendEmailAsync(dto, ModelState);

            if (!result)
            {
                return View(dto);
            }

            return RedirectToAction(nameof(ThankYou));

        }


        public IActionResult ThankYou()
        {
            return View();
        }

    }
}
