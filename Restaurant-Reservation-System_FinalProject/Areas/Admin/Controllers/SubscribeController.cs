using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant_Reservation_System_.Service.Dtos.SubscribeDtos;
using Restaurant_Reservation_System_.Service.Services.IService;

namespace Restaurant_Reservation_System_FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Superadmin")]
    public class SubscribeController : Controller
    {
        private readonly ISubscribeService _subscribeService;
        public SubscribeController(ISubscribeService subscribeService)
        {
            _subscribeService = subscribeService;            
        }
        public async Task<IActionResult> Index()
        {
            var result = await _subscribeService.GetAllAsync();

            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Create(SubscribeCreateDto dto)
        {
            var result = await _subscribeService.CreateAsync(dto, ModelState);

            if (result is false)
                return View(dto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var result = await _subscribeService.GetUpdatedDtoAsync(id);

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SubscribeUpdateDto dto)
        {
            var result = await _subscribeService.UpdateAsync(dto, ModelState);

            if (result is false)
                return View(dto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _subscribeService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

    }
}
    