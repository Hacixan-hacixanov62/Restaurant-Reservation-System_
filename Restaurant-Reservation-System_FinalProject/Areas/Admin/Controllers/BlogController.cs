using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.DataAccess.Helpers;
using Restaurant_Reservation_System_.Service.Dtos.BlogDtos;
using Restaurant_Reservation_System_.Service.Services.IService;

namespace Restaurant_Reservation_System_FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Superadmin")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ITopicService _topicService;
        private readonly IChefService _chefService;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public BlogController(IBlogService blogService, AppDbContext context, IMapper mapper, ITopicService topiccService, IChefService chefService)
        {
            _blogService = blogService;
            _context = context;
            _mapper = mapper;
            _topicService = topiccService;
            _chefService = chefService;
        }
        public async Task<IActionResult> Index(int page = 1, int take = 2)
        {
            try
            {
                var blogs = _context.Blogs
                    .Include(c => c.Chef)
                    .Include(c => c.BlogTopics)
                    .AsQueryable();
                PaginatedList<Blog> paginatedList = PaginatedList<Blog>.Create(blogs, take, page);
                return View(paginatedList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> Create()
        {
            var topic = await _topicService.GetAllAsync();
            var chef = await _chefService.GetAllAsync();

            ViewBag.Topics = new SelectList(topic, nameof(Topic.Id), nameof(Topic.Name));
            ViewBag.Chefs = new SelectList(chef, nameof(Chef.Id), nameof(Chef.Name));

            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] BlogCreateDto blogCreateDto)
        {

            ViewBag.Topics = await _context.Topics.ToListAsync();
            ViewBag.Chef = await _context.Chefs.ToListAsync();

            if (!ModelState.IsValid)
            {
                return View(blogCreateDto);
            }

            var isExistAuth = await _context.Chefs.AnyAsync(x => x.Id == blogCreateDto.ChefId);
            if (!isExistAuth)
            {
                ModelState.AddModelError("ChefId", "Chef is not found");
                return View(blogCreateDto);
            }


            foreach (var topic in blogCreateDto.TopicIds)
            {
                var isExistTopic = await _context.Topics.AnyAsync(x => x.Id == topic);
                if (!isExistTopic)
                {
                    ModelState.AddModelError("TopicIds", "Topic is not found");
                    return View(blogCreateDto);
                }
            }

            if (_context.Blogs.Any(x => x.Title == blogCreateDto.Title))
            {
                ModelState.AddModelError("", "Blog already exists");
                return View(blogCreateDto);
            }

            await _blogService.CreateAsync(blogCreateDto);
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
            if (id < 1)
            {
                return NotFound();
            }
            var topic = await _topicService.GetAllAsync();
            var chef = await _chefService.GetAllAsync();

            ViewBag.Topics = new SelectList(topic, nameof(Topic.Id), nameof(Topic.Name));
            ViewBag.Chefs = new SelectList(chef, nameof(Chef.Id), nameof(Chef.Name));

            var blog = await _context.Blogs.Include(x => x.BlogTopics)
                                           .Include(x => x.Chef)
                                           .FirstOrDefaultAsync(x => x.Id == id);

            if (blog == null) return NotFound();

            BlogUpdateDto blogUpdateDto = _mapper.Map<BlogUpdateDto>(blog);
            blogUpdateDto.TopicIds = blog.BlogTopics.Select(x => x.TopicId).ToList();

            return View(blogUpdateDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BlogUpdateDto blogUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(blogUpdateDto);
            }

            ViewBag.Topics = new SelectList(await _context.Topics.ToListAsync(), nameof(Topic.Id), nameof(Topic.Name));
            ViewBag.Chefs = new SelectList(await _context.Chefs.ToListAsync(), nameof(Chef.Id), nameof(Chef.Name));

            try
            {
                var isExist = await _context.Blogs.AnyAsync(x => x.Title == blogUpdateDto.Title && x.Id != id);
                if (isExist)
                {
                    ViewBag.Topics = new SelectList(await _context.Topics.ToListAsync(), nameof(Topic.Id), nameof(Topic.Name));
                    ViewBag.Chefs = new SelectList(await _context.Chefs.ToListAsync(), nameof(Chef.Id), nameof(Chef.Name));

                    ModelState.AddModelError("", "Blog already exists");
                    return View(blogUpdateDto);
                }


                var isExistAuth = await _context.Chefs.AnyAsync(x => x.Id == blogUpdateDto.ChefId);
                if (!isExistAuth)
                {
                    ViewBag.Topics = new SelectList(await _context.Topics.ToListAsync(), nameof(Topic.Id), nameof(Topic.Name));
                    ViewBag.Chefs = new SelectList(await _context.Chefs.ToListAsync(), nameof(Chef.Id), nameof(Chef.Name));

                    ModelState.AddModelError("AuthorId", "Author is not found");
                    return View(blogUpdateDto);
                }


                foreach (var topic in blogUpdateDto.TopicIds)
                {
                    var isExistTopic = await _context.Topics.AnyAsync(x => x.Id == topic);
                    if (!isExistTopic)
                    {
                        ViewBag.Topics = new SelectList(await _context.Topics.ToListAsync(), nameof(Topic.Id), nameof(Topic.Name));
                        ViewBag.Chefs = new SelectList(await _context.Chefs.ToListAsync(), nameof(Chef.Id), nameof(Chef.Name));

                        ModelState.AddModelError("TopicIds", "Topic is not found");
                        return View(blogUpdateDto);
                    }
                }

                await _blogService.EditAsync(id, blogUpdateDto);
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _blogService.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message); // Slider tapılmadıqda
            }
        }


        [HttpGet("admin/Blog/detail")]
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var produc = await _blogService.DetailAsync(id);
                return View(produc);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message); // Slider tapılmadıqda
            }
        }
    }
}
