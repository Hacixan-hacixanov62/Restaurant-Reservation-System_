using AutoMapper;
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
using System.Linq;

namespace Restaurant_Reservation_System_FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Superadmin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ProductController(IProductService productService,AppDbContext context,IMapper mapper)
        {
            _productService = productService;
            _context = context;
            _mapper = mapper;
        }



        public async Task<IActionResult> Index(int page = 1, int take = 2)
        {
            
            try
            {
                var categories = _context.Products
                    .Include(c => c.ProductDetails)
                    .Include(c => c.ProductImages)
                    .Include(c => c.Category)
                    .OrderByDescending(c => c.CreatedAt)
                    .AsQueryable();
                PaginatedList<Product> paginatedList = PaginatedList<Product>.Create(categories, take, page);
                return View(paginatedList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ProductCreateDto productCreateDto)
        {
            ViewBag.Categories = _context.Categories.ToList();

            if (!ModelState.IsValid)
            {
                return View();
            }

            var isExistCategory = await _context.Categories.AnyAsync(x => x.Id == productCreateDto.CategoryId);
            if (!isExistCategory)
            {
                ModelState.AddModelError("CategoryId", "Category is not found");
                return View(productCreateDto);
            }


            if (_context.Products.Any(x => x.Name == productCreateDto.Name))
            {
                ModelState.AddModelError("", "Product already exists");
                return View(productCreateDto);
            }

            await _productService.CreateAsync(productCreateDto);
            return RedirectToAction(nameof(Index));


        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id < 1) return NotFound();
            ViewBag.Categories = await _context.Categories.ToListAsync();


            var product = await _productService.DetailAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            ProductUpdateDto dto = _mapper.Map<ProductUpdateDto>(product);


            dto.ImagePaths = product.ProductImages.Where(x => !x.IsMain).Select(x => x.Url).ToList();
            dto.ImageIds = product.ProductImages.Where(x => !x.IsMain).Select(x => x.Id).ToList();
            dto.MainFileUrl = product.ProductImages.FirstOrDefault(x => x.IsMain)?.Url ?? "null";
            return View(dto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductUpdateDto productUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(productUpdateDto);
            }

            var category =  _context.Categories.FirstOrDefault(x => x.Id == productUpdateDto.CategoryId);

            if (category == null)
            {
                return View(productUpdateDto);
            }

            ViewBag.Categories = await _context.Categories.ToListAsync();
            try
            {

                var existProduct = await _context.Products.Include(x => x.ProductImages).FirstOrDefaultAsync(x => x.Id == id);

                if (existProduct is null)
                {
                    return NotFound();
                }

                var isExist = await _context.Products.AnyAsync(x => x.Name == productUpdateDto.Name && x.Id != id);
                if (isExist)
                {
                    ModelState.AddModelError("Name", "Product already exists");
                    return View(productUpdateDto);
                }

                var isExistCategory = await _context.Categories.AnyAsync(x => x.Id == productUpdateDto.CategoryId);
                if (!isExistCategory)
                {
                    ModelState.AddModelError("CategoryId", "Category is not found");
                    return View(productUpdateDto);
                }

                await _productService.EditAsync(id, productUpdateDto);
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(productUpdateDto);
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
            if (productImage.IsMain == true)
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
            var mainImage = _context.ProductImages.FirstOrDefault(b => b.IsMain == true && b.ProductId == productImage.ProductId);
            //mainImage.IsMain = null;
            productImage.IsMain = true;

            _context.SaveChanges();
            return RedirectToAction("edit", new { id = productImage.ProductId });
        }


        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return RedirectToAction("Index");

        }


        [HttpGet("admin/Product/detail")]
        public async Task<IActionResult> Detail(int id)
        {
            var produc = await _productService.DetailAsync(id);
            return View(produc);
        }

    }
}
