using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.Service.UI.VM;

namespace Restaurant_Reservation_System_FinalProject.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? chefId)
        {
           
            var topics = await _context.Topics.ToListAsync();
            var result = _context.Blogs.Include(x => x.Chef)
                                       .Include(x => x.BlogTopics).ThenInclude(x => x.Topic);

            List<Blog> blogs = new();
            
            if (chefId is not null)
            {
                blogs = await result.Where(x => x.BlogTopics.Any(y => y.TopicId == chefId)).ToListAsync();

            }
            else
            {
                blogs = await result.ToListAsync();
            }

            BlogVM blogVM = new BlogVM()
            {
                Blogs = blogs,
                Topics = topics
            };


            return View(blogVM);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var topics = await _context.Topics.ToListAsync();
            var blog = await _context.Blogs.Include(x => x.Chef).Include(x => x.BlogTopics).ThenInclude(x => x.Topic).FirstOrDefaultAsync(x => x.Id == id);


            if (blog is null)
                return NotFound();

            BlogDetailVM blogVm = new()
            {
                Blog = blog,
                Topics = topics

            };

            blogVm.NextBlog = await _context.Blogs.FirstOrDefaultAsync(x => x.Id > id);
            blogVm.PrevBlog = await _context.Blogs.OrderByDescending(x => x.Id).FirstOrDefaultAsync(x => x.Id < id);

            if (blogVm.NextBlog is null)
                blogVm.NextBlog = await _context.Blogs.FirstOrDefaultAsync(x => x.Id < id);

            if (blogVm.PrevBlog is null)
                blogVm.PrevBlog = await _context.Blogs.OrderByDescending(x => x.Id).FirstOrDefaultAsync(x => x.Id > id);

            return View(blogVm);
        }
    }
}
