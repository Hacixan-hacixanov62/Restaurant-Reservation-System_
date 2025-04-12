using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.Service.Dtos.CategoryDtos;
using Restaurant_Reservation_System_.Service.Dtos.CommentDtos;
using Restaurant_Reservation_System_.Service.Dtos.ProductDtos;
using Restaurant_Reservation_System_.Service.Extensions;
using Restaurant_Reservation_System_.Service.Services.IService;
using Restaurant_Reservation_System_.Service.UI.Dtos;

namespace Restaurant_Reservation_System_FinalProject.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;
        public ShopController(IProductService productService,ICategoryService categoryService, ICommentService commentService,IMapper mapper)
        {
            _productService = productService;
            _categoryService = categoryService;
            _commentService = commentService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string sortOrder)
        {
            //var shops = _context.Products
            //    .Include(x => x.ProductImages.Where(x => x.IsMain == true))
            //    .Include(x => x.ProductDetails).AsQueryable();

            var products = await _productService.GetAllAsync();
            var categories = await _categoryService.GetAllAsync();

            sortOrder ??= "Default";

            products = sortOrder switch
            {
                "PriceLowToHigh" => products.OrderBy(p => p.Price).ToList(),
                "PriceHighToLow" => products.OrderByDescending(p => p.Price).ToList(),
                _ => products.OrderBy(p => p.Name).ToList(),
            };

            ViewData["SelectedSort"] = sortOrder;   

            var productDtos = _mapper.Map<List<ProductGetDto>>(products);
            var categoryDtos = _mapper.Map<List<CategoryGetDto>>(categories);
            var shopDto = new ShopDto
            {
                Products = productDtos,
                Categories = categoryDtos
            };

            return View(shopDto);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var comments = await _commentService.GetProductCommentsAsync(id);
            var isAllowComment = await _commentService.CheckIsAllowCommentAsync(id);

            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            
            ShopDetailDto dto = new()
            {
                Comments = comments,
                IsAllowComment = isAllowComment,
                Product =product
            };

            return View(dto);
        }


        [HttpPost]
        public async Task<IActionResult> CreateComment(CommentCreateDto dto)
        {
            var result = await _commentService.CreateAsync(dto, ModelState);

            string returnUrl = Request.GetReturnUrl();

            return Redirect(returnUrl);
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> ReplyComment(CommentReplyDto dto)
        {

            var result = await _commentService.CreateReplyAsync(dto, ModelState);

            string returnUrl = Request.GetReturnUrl();

            return Redirect(returnUrl);
        }


        public async Task<IActionResult> DeleteComment(int id)
        {
            await _commentService.DeleteAsync(id);

            string returnUrl = Request.GetReturnUrl();

            return Redirect(returnUrl);
        }
    }
}
