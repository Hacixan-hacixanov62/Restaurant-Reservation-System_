using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.DataAccess.Helpers;
using Restaurant_Reservation_System_.Service.Dtos.CategoryDtos;
using Restaurant_Reservation_System_.Service.Dtos.ChefDtos;
using Restaurant_Reservation_System_.Service.Dtos.ProductDtos;
using Restaurant_Reservation_System_.Service.Services;
using Restaurant_Reservation_System_.Service.Services.IService;

namespace Restaurant_Reservation_System_FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Superadmin")]
    public class ChefController : Controller
    {
        private readonly IChefService _chefService;
        private readonly AppDbContext _context;
        public ChefController(IChefService chefService, AppDbContext context)
        {
            _chefService = chefService;
            _context = context;

        }

        public async Task<IActionResult> Index(int page = 1)
        {
            int pageCount = (int)Math.Ceiling((decimal)_context.Chefs.Count() / 10);

            if (pageCount == 0)
                pageCount = 1;

            ViewBag.PageCount = pageCount;

            if (page > pageCount)
                page = pageCount;

            if (page <= 0)
                page = 1;

            ViewBag.CurrentPage = page;

            var authors = await _context.Chefs.OrderByDescending(x => x.Id).Skip((page - 1) * 10).Take(10).ToListAsync();
            return View(authors);
            ////try
            ////{
            ////    var categories = _context.Chefs.Include(m => m.).ToListAsync();
            ////    PaginatedList<Chef> paginatedList = PaginatedList<Chef>.Create(categories, take, page);
            ////    return View(paginatedList);
            ////    //var chefs = _chefService.GetAllAsync();
            ////    //return View(chefs);
            ////}

            ////catch (Exception ex)
            ////{
            ////    return BadRequest(ex.Message);
            ////}

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ChefCreateDto chefCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(chefCreateDto);
            }
            var isExistDescription = await _context.Chefs.AnyAsync(x => x.Description.ToLower() == chefCreateDto.Description.ToLower());
            var isExistBiographia = await _context.Chefs.AnyAsync(x => x.Biographia.ToLower() == chefCreateDto.Biographia.ToLower());


            if (isExistDescription)
            {
                ModelState.AddModelError("Description", "Description already exist");
                return View(chefCreateDto);
            }
            if (isExistBiographia)
            {
                ModelState.AddModelError("Biographia", "Biographia already exist");
                return View(chefCreateDto);
            }

            await _chefService.CreateAsync(chefCreateDto);
            return RedirectToAction(nameof(Index));
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

            var chef = await _chefService.DetailAsync(id.Value);
            if (chef == null)
            {
                return NotFound();
            }

            return View(new ChefUpdateDto
            {
                Name = chef.Name,
                Surname = chef.Surname,
                Description = chef.Description,
                Biographia = chef.Biographia,
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,ChefUpdateDto chefUpdateDto)
        {

            try
            {
                var existChef = await _context.Chefs.FirstOrDefaultAsync(x => x.Id == id);
                if (existChef is null)
                    return BadRequest();

                if (!ModelState.IsValid)
                    return View(chefUpdateDto);

                var isExistBiographia = await _context.Chefs.AnyAsync(x => x.Biographia.ToLower() == chefUpdateDto.Biographia.ToLower() && x.Id != id);

                if (isExistBiographia)
                {
                    ModelState.AddModelError("Biographia", "Biographia already exist");
                    return View(chefUpdateDto);
                }

                await _chefService.EditAsync(id, chefUpdateDto);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(chefUpdateDto);
            }

        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _chefService.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpGet("admin/chef/detail")]
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var slider = await _chefService.DetailAsync(id);
                return View(slider);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message); // Slider tapılmadıqda
            }
        }


    }
}
