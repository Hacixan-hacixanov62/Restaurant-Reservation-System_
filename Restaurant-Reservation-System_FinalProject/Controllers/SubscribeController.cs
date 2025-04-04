using Microsoft.AspNetCore.Mvc;
using Restaurant_Reservation_System_.Service.Dtos.SubscribeDtos;
using Restaurant_Reservation_System_.Service.Services.IService;

namespace Restaurant_Reservation_System_FinalProject.Controllers
{
    public class SubscribeController : Controller
    {
        private readonly ISubscribeService _subscribeService;
        public SubscribeController(ISubscribeService subscribeService)
        {
            _subscribeService = subscribeService;
        }
        [HttpPost]
        public async Task<IActionResult> Subscribe(SubscribeCreateDto dto)
        {
            var result = await _subscribeService.CreateAsync(dto, ModelState);

            if (result is false)
                return View(dto);

            return RedirectToAction(nameof(Index));
        }
    }
}
