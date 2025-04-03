using AutoMapper;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.DataAccess.Helpers;
using Restaurant_Reservation_System_.Service.Dtos.CategoryDtos;
using Restaurant_Reservation_System_.Service.Services;
using Restaurant_Reservation_System_.Service.Services.IService;
using Restaurant_Reservation_System_.Service.ViewModels.CategoryVM;
using Restaurant_Reservation_System_.Service.ViewModels.SliderVM;

namespace Restaurant_Reservation_System_FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Superadmin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryService categoryService,AppDbContext context,IMapper mapper)
        {
            _categoryService = categoryService;
            _context = context;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(int page = 1, int take = 2)
        {   

            try
            {
                var categories = _context.Categories.Include(c => c.Products).AsQueryable();
                PaginatedList<Category> paginatedList = PaginatedList<Category>.Create(categories, take, page);
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
        public async Task<IActionResult> Create([FromForm] CategoryCreateDto categoryCreateDto)
        {
            var isExistTitle = await _context.Categories.AnyAsync(x => x.Title.ToLower() == categoryCreateDto.Title.ToLower());
            var isExistSubTitle = await _context.Categories.AnyAsync(x => x.SubTitle.ToLower() == categoryCreateDto.SubTitle.ToLower());

            if (isExistTitle)
            {
                ModelState.AddModelError("Title", "Title alredy exist");
                return View(categoryCreateDto);
            }
            if (isExistSubTitle)
            {
                ModelState.AddModelError("SubTitle", "Subtitle alredy exist");
                return View(categoryCreateDto);
            }

            await _categoryService.CreateAsync(categoryCreateDto);
            return RedirectToAction("Index");


            //try
            //{

            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryService.DetailAsync(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            return View(new CategoryUpdateDto
            {
                Title = category.Title,
                SubTitle = category.SubTitle
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryUpdateDto categoryUpdateDto)
        {
         
            try
            {
                var existCategory = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (existCategory is null)
                    return BadRequest();

                if (!ModelState.IsValid)
                    return View(categoryUpdateDto);

                var isExistTitle = await _context.Categories.AnyAsync(x => x.Title.ToLower() == categoryUpdateDto.Title.ToLower() && x.Id != id);
                var isExistSubTitle = await _context.Categories.AnyAsync(x => x.SubTitle.ToLower() == categoryUpdateDto.SubTitle.ToLower() && x.Id != id);

                if (isExistTitle)
                {
                    ModelState.AddModelError("Title", "Title alredy exist");
                    return View(categoryUpdateDto);
                }
                if (isExistSubTitle)
                {
                    ModelState.AddModelError("SubTitle", "Subtitle alredy exist");
                    return View(categoryUpdateDto);
                }

                existCategory = _mapper.Map(categoryUpdateDto, existCategory);


                await _categoryService.EditAsync(id, categoryUpdateDto);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(categoryUpdateDto);
            }

        }


        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _categoryService.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpGet("admin/category/detail")]
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var slider = await _categoryService.DetailAsync(id);
                return View(slider);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message); // Slider tapılmadıqda
            }
        }


    }
}
