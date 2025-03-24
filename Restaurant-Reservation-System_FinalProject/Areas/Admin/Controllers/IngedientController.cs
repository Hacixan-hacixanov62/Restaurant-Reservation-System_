using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.DataAccess.Helpers;
using Restaurant_Reservation_System_.Service.Services;
using Restaurant_Reservation_System_.Service.Services.IService;
using Restaurant_Reservation_System_.Service.ViewModels.CategoryVM;
using Restaurant_Reservation_System_.Service.ViewModels.IngrideantVM;

namespace Restaurant_Reservation_System_FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Superadmin")]
    public class IngedientController : Controller
    {
        private readonly IIngridentService _ingridentService;
        private readonly AppDbContext _context;
        public IngedientController(IIngridentService ingridentService,AppDbContext context)
        {
            _context = context;
            _ingridentService = ingridentService;
        }

        public async Task<IActionResult> Index(int page = 1, int take = 2)
        {

            try
            {
                var ingredients = _context.Ingredients.Include(c => c.ProductIngredients).AsQueryable();
                PaginatedList<Ingredient> paginatedList = PaginatedList<Ingredient>.Create(ingredients, take, page);
                return View(paginatedList);
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
        public async Task<IActionResult> Create([FromForm] IngredientCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _ingridentService.CreateAsync(model);
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

            var category = await _ingridentService.DetailAsync(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            return View(new IngredientEditVM
            {
                Name = category.Name
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IngredientEditVM ingredientEditVM)
        {
            if (!ModelState.IsValid)
            {
                return View(ingredientEditVM);
            }

            try
            {
                await _ingridentService.EditAsync(id, ingredientEditVM);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(ingredientEditVM);
            }

        }


        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _ingridentService.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpGet("admin/Ingredient/detail")]
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var slider = await _ingridentService.DetailAsync(id);
                return View(slider);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message); // Slider tapılmadıqda
            }
        }


    }
}
