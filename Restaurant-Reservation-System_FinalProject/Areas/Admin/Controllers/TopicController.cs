using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.DataAccess.Helpers;
using Restaurant_Reservation_System_.Service.Dtos.CategoryDtos;
using Restaurant_Reservation_System_.Service.Dtos.TopicDtos;
using Restaurant_Reservation_System_.Service.Services;
using Restaurant_Reservation_System_.Service.Services.IService;

namespace Restaurant_Reservation_System_FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Superadmin")]
    public class TopicController : Controller
    {
        private readonly ITopicService _topicService;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public TopicController(AppDbContext context, IMapper mapper, ITopicService topicService)
        {
            _context = context;
            _mapper = mapper;
            _topicService = topicService;
        }
        public async Task<IActionResult> Index(int page = 1, int take = 2)
        {

            try
            {
                var categories = _context.Topics.Include(c => c.BlogTopics).AsQueryable();
                PaginatedList<Topic> paginatedList = PaginatedList<Topic>.Create(categories, take, page);
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
        public async Task<IActionResult> Create([FromForm] TopicCreateDto topicCreateDto)
        {

            try
            {
                var isExistTitle = await _context.Topics.AnyAsync(x => x.Name.ToLower() == topicCreateDto.Name.ToLower());
                if (isExistTitle)
                {
                    ModelState.AddModelError("Name", "Name alredy exist");
                    return View(topicCreateDto);
                }

                await _topicService.CreateAsync(topicCreateDto);
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

            var category = await _topicService.DetailAsync(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            return View(new TopicUpdateDto
            {
              Name = category.Name,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TopicUpdateDto topicUpdateDto)
        {
            try
            {
                var existCategory = await _context.Topics.FirstOrDefaultAsync(x => x.Id == id);
                if (existCategory is null)
                    return BadRequest();

                if (!ModelState.IsValid)
                    return View(topicUpdateDto);

                var isExistTitle = await _context.Topics.AnyAsync(x => x.Name.ToLower() == topicUpdateDto.Name.ToLower() && x.Id != id);
                if (isExistTitle)
                {
                    ModelState.AddModelError("Name", "Name alredy exist");
                    return View(topicUpdateDto);
                }

                existCategory = _mapper.Map(topicUpdateDto, existCategory);


                await _topicService.EditAsync(id, topicUpdateDto);
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(topicUpdateDto);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _topicService.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
