using Microsoft.AspNetCore.Mvc;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.DataAccess.Helpers;

namespace Restaurant_Reservation_System_FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Sliders.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Slider slider)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}

            if (slider.Photo == null)
            {
                ModelState.AddModelError("Photo", " Photo is required");
            }

            var file = slider.Photo;


            slider.Image = file.SaveImage(_env.WebRootPath, "assets/images/home");
            _context.Sliders.Add(slider);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }
            var slider = _context.Sliders.Find(id);
            if (slider is null)
            {
                return NotFound();
            }
            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Slider slider)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(slider);
            //}
            var existSlider = _context.Sliders.Find(slider.Id);
            if (existSlider is null)
            {
                return NotFound();
            }

            var file = slider.Photo;
            string oldImage = existSlider.Image;
            if (file != null)
            {

                existSlider.Image = file.SaveImage(_env.WebRootPath, "assets/images/home");
                var deleteImagePath = Path.Combine(_env.WebRootPath, "assets/images/home", oldImage);

                FileManager.DeleteFile(deleteImagePath);
            }

            existSlider.Title = slider.Title;
            existSlider.Description = slider.Description;
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        public IActionResult Delete(int? id)
        {
            var existSlider = _context.Sliders.Find(id);
            if (existSlider is null)
            {
                return NotFound();
            }
            _context.Sliders.Remove(existSlider);
            _context.SaveChanges();


            var deleteImagePath = Path.Combine(_env.WebRootPath, "assets/images/home", existSlider.Image);
            FileManager.DeleteFile(deleteImagePath);


            return RedirectToAction("Index");
        }


        [HttpGet("admin/slider/detail")]
        public IActionResult Detail(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var slider = _context.Sliders
                         .Where(s => s.Id == id)
                        .FirstOrDefault();
            if (slider is null)
            {
                return NotFound();
            }
            return View(slider);
        }
    }
}
