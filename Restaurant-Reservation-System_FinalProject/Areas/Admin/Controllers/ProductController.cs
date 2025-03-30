using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.DataAccess.Helpers;
using Restaurant_Reservation_System_.Service.Dtos.ProductDtos;
using Restaurant_Reservation_System_.Service.Services;
using Restaurant_Reservation_System_.Service.Services.IService;
using Restaurant_Reservation_System_.Service.ViewModels.ProductVM;
using Restaurant_Reservation_System_.Service.ViewModels.SliderVM;

namespace Restaurant_Reservation_System_FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Superadmin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly AppDbContext _context;
        public ProductController(IProductService productService,AppDbContext context)
        {
            _productService = productService;
            _context = context;
        }



        public async Task<IActionResult> Index(int page = 1, int take = 2)
        {
            
            try
            {
                var categories = _context.Products
                    .Include(c => c.ProductDetails)
                    .Include(c => c.ProductImages)
                    .Include(c => c.Category)
                    .AsQueryable();
                PaginatedList<Product> paginatedList = PaginatedList<Product>.Create(categories, take, page);
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
        public async Task<IActionResult> Create([FromForm] Product model)
        {
            ViewBag.Categories = _context.Categories.ToList();

            if (!_context.Categories.Any(g => g.Id == model.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Category not found");
                return View();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _productService.CreateAsync(model);
                return RedirectToAction(nameof(Index));
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

            var product = await _productService.DetailAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(new Product
            {
                Name = product.Name,
                Desc = product.Desc,
                Price = product.Price,
                ProductImages = product.ProductImages
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product sliderEditVM)
        {
            if (!ModelState.IsValid)
            {
                return View(sliderEditVM);
            }

            try
            {
                await _productService.EditAsync(id, sliderEditVM);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(sliderEditVM);
            }

        }


        public IActionResult DeleteProductImage(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }
            var productImage = _context.ProductImages.Find(id);
            if (productImage is null)
            {
                return NotFound();
            }
            if (productImage.Status == true)
            {
                return BadRequest("");
            }
            _context.ProductImages.Remove(productImage);
            _context.SaveChanges();
            return RedirectToAction("edit", new { id = productImage.ProductId });
        }

        public IActionResult SetMainImage(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }
            var productImage = _context.ProductImages.Find(id);
            if (productImage is null)
            {
                return NotFound();
            }
            var mainImage = _context.ProductImages.FirstOrDefault(b => b.Status == true && b.ProductId == productImage.ProductId);
            mainImage.Status = null;
            productImage.Status = true;

            _context.SaveChanges();
            return RedirectToAction("edit", new { id = productImage.ProductId });
        }


        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _productService.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message); // Slider tapılmadıqda
            }
        }


        [HttpGet("admin/Product/detail")]
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var produc = await _productService.DetailAsync(id);
                return View(produc);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message); // Slider tapılmadıqda
            }
        }

    }
}
