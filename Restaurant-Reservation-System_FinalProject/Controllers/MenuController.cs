﻿
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.Service.UI.VM;

namespace Restaurant_Reservation_System_FinalProject.Controllers
{
    public class MenuController : Controller
    {

        private readonly AppDbContext _context;
        public MenuController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? categoryId)
        {

            if (categoryId is not null)
            {
                var category = await _context.Categories.Include(x => x.Products).ThenInclude(x => x.ProductImages).FirstOrDefaultAsync(x => x.Id == categoryId);

                if (category is null || category.Products.Count() == 0)
                    return NotFound();

                return View(new List<Category>() { category });

            }
            var categories = await _context.Categories.Include(x => x.Products).ThenInclude(x => x.ProductImages).Where(x => x.Products.Count > 0).ToListAsync();
            return View(categories);
        }


        //Asagdaki Actionu men Fake MenuLinki Yeni rezerv olunanda musteri QR Code skan edende ona menyunu gosterir

        [HttpGet("fakeMenuLink/{bookingId}")]
        public IActionResult FakeMenuLink(string bookingId)
        {

            return RedirectToAction("Index", "Menu");
        }
    }
}
