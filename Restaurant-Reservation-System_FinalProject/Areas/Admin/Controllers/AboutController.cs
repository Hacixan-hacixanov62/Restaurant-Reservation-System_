using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant_Reservation_System_.Service.Services;
using Restaurant_Reservation_System_.Service.Services.IService;
using Restaurant_Reservation_System_.Service.ViewModels.AboutVM;
using Restaurant_Reservation_System_.Service.ViewModels.SliderVM;

namespace Restaurant_Reservation_System_FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;
        public AboutController(IAboutService aboutService)
        {        
            _aboutService = aboutService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var abouts = await _aboutService.GetAllAsync();
                return View(abouts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] AboutCreateVM aboutCreateVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(aboutCreateVM);
            }

            try
            {
                await _aboutService.CreateAsync(aboutCreateVM);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var about = await _aboutService.DetailAsync(id.Value);
            if (about == null)
            {
                return NotFound();
            }

            return View(new AboutEditVM
            {
                Title = about.Title,
                Description = about.Desc,
                Image = null
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AboutEditVM aboutEditVM)
        {
            if (!ModelState.IsValid)
            {
                return View(aboutEditVM);
            }

            try
            {
                await _aboutService.EditAsync(id, aboutEditVM);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(aboutEditVM);
            }

        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _aboutService.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message); 
            }
        }

    }
}
