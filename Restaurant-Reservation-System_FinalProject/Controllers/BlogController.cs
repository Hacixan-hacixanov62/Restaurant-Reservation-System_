using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.Core.Enums;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.Service.Dtos.BlogCommentDtos;
using Restaurant_Reservation_System_.Service.Extensions;
using Restaurant_Reservation_System_.Service.Services.IService;
using Restaurant_Reservation_System_.Service.UI.VM;

namespace Restaurant_Reservation_System_FinalProject.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBlogService _blogService;
        private readonly IBlogCommentService _commentService;

        public BlogController(AppDbContext context, IBlogCommentService commentService, IBlogService blogService)
        {
            _context = context;
            _commentService = commentService;
            _blogService = blogService;
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
            var comments = await _commentService.GetBlogCommentsAsync(id);
            var isAllowBlogComment = await _commentService.CheckIsAllowBlogCommentAsync(id);

            var allBlogs = await _blogService.GetAllAsync();
            var currentIndex = allBlogs.FindIndex(b => b.Id == id);

            // Növbəti və əvvəlki blogları tapın
            var nextBlogId = currentIndex < allBlogs.Count - 1 ? allBlogs[currentIndex + 1].Id : (int?)null;
            var prevBlogId = currentIndex > 0 ? allBlogs[currentIndex - 1].Id : (int?)null;


            if (blog is null)
                return NotFound();

            BlogDetailVM blogVm = new()
            {
                Blog = blog,
                Topics = topics,
                BlogComments = comments,
                IsAllowBlogComment = isAllowBlogComment,
                NextBlogId = nextBlogId,
                PrevBlogId = prevBlogId
            };

            blogVm.NextBlog = await _context.Blogs.FirstOrDefaultAsync(x => x.Id > id);
            blogVm.PrevBlog = await _context.Blogs.OrderByDescending(x => x.Id).FirstOrDefaultAsync(x => x.Id < id);

            if (blogVm.NextBlog is null)
                blogVm.NextBlog = await _context.Blogs.FirstOrDefaultAsync(x => x.Id < id);

            if (blogVm.PrevBlog is null)
                blogVm.PrevBlog = await _context.Blogs.OrderByDescending(x => x.Id).FirstOrDefaultAsync(x => x.Id > id);

            return View(blogVm);
        }


        [HttpPost]
        public async Task<IActionResult> CreateBlogComment(BlogCommentCreateDto dto)
        {
            var result = await _commentService.CreateAsync(dto, ModelState);

            string returnUrl = Request.GetReturnUrl();

            return Redirect(returnUrl);
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> ReplyComment(BlogCommentReplyDto dto)
        {

            var result = await _commentService.CreateReplyAsync(dto, ModelState);

            string returnUrl = Request.GetReturnUrl();

            return Redirect(returnUrl);
        }

        public async Task<IActionResult> DeleteBlogComment(int id)
        {
            await _commentService.DeleteAsync(id);

            string returnUrl = Request.GetReturnUrl();

            return Redirect(returnUrl);
        }


        [HttpPost]
        public IActionResult SubscribeNewsletter(string mc_email)
        {
            if (string.IsNullOrEmpty(mc_email))
            {
                // Əgər e-poçt boşdursa, istifadəçini səhifəyə yenidən qaytarın və xəbərdarlıq göstərin
                TempData["Error"] = "Please enter a valid email address.";
                return Redirect(Request.Headers["Referer"].ToString() + "#newsletter"); // Səhifəyə geri dön və `newsletter` hissəsinə fokuslan
            }

            // Newsletter üçün əlavə məntiq (məsələn, email saxlama və ya API çağırışı)
            TempData["Success"] = "Thank you for subscribing to our newsletter!";

            // İstifadəçini yenidən səhifəyə yönləndir və `newsletter` hissəsinə fokuslan
            return Redirect(Request.Headers["Referer"].ToString() + "#newsletter");
        }
    

    }
}
