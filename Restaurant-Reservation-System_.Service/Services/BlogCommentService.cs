
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.Core.Enums;
using Restaurant_Reservation_System_.DataAccess.Localizers;
using Restaurant_Reservation_System_.DataAccess.Repositories;
using Restaurant_Reservation_System_.DataAccess.Repositories.IRepositories;
using Restaurant_Reservation_System_.Service.Dtos.BlogCommentDtos;
using Restaurant_Reservation_System_.Service.Exceptions;
using Restaurant_Reservation_System_.Service.Services.IService;
using System.Security.Claims;

namespace Restaurant_Reservation_System_.Service.Services
{
    public class BlogCommentService : IBlogCommentService
    {
        private readonly IBlogCommentRepository _blogCommentRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IBlogService _blogService;
        private readonly IOrderService _orderService;

        public BlogCommentService(IBlogCommentRepository blogCommentRepository, IMapper mapper, IHttpContextAccessor contextAccessor, IBlogService blogService, IOrderService orderService)
        {
            _blogCommentRepository = blogCommentRepository;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _blogService = blogService;
            _orderService = orderService;
        }

        public async Task<bool> CheckIsAllowBlogCommentAsync(int blogId)
        {
            var blog = await _blogService.GetAllAsync();

            var orders = await _orderService.GetAllAsync();

            var userId = _getUserId();


            var isExist = await _blogCommentRepository.IsExistAsync(x => x.BlogId == blogId && x.AppUserId == userId);

            if (isExist)
                return false;

            return true;
        }

        public async Task<bool> CreateAsync(BlogCommentCreateDto dto, ModelStateDictionary ModelState)
        {
            if (!ModelState.IsValid)
                return false;

            if (!_checkAuthorized())
                throw new UnAuthorizedException("NotFound Comment");

            var blog = await _blogService.GetAllAsync();

            var orders = await _orderService.GetAllAsync();

            var userId = _getUserId();


            //var isExist = await _blogCommentRepository.IsExistAsync(x => x.BlogId == dto.BlogId && x.AppUserId == userId);

            //if (isExist)                                                         // Bunu Agilden sorusarsan Blogun Alreadysini yazanda ikinci defe blog yazmaq isteyirem alinmir exception verir duzeldersen
            //    throw new AlreadyExistException("Commnet is Already Exist");

            var comment = _mapper.Map<BlogComment>(dto);

            comment.AppUserId = userId;
            comment.CreatedBy = _contextAccessor.HttpContext?.User.Identity?.Name ?? "System"; // İstifadəçi adı və ya "System"
            comment.UpdatedBy = _contextAccessor.HttpContext?.User.Identity?.Name ?? "System"; // İstifadəçi adı və ya "System"


            await _blogCommentRepository.CreateAsync(comment);
            await _blogCommentRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CreateReplyAsync(BlogCommentReplyDto dto, ModelStateDictionary ModelState)
        {
            if (!ModelState.IsValid)
                return false;

            if (!_checkAuthorized())
                throw new UnAuthorizedException("Blog Comment NotFound");

            var blog = await _blogService.GetAllAsync();

            var orders = await _orderService.GetAllAsync();

            var userId = _getUserId();

            var parentComment = await _blogCommentRepository.GetAsync(dto.ParentId);
            if (parentComment == null) throw new NotFoundException("Parent comment not found");

            var replyComment = _mapper.Map<BlogComment>(dto);
            replyComment.AppUserId = userId;
            replyComment.CreatedBy = _contextAccessor.HttpContext?.User.Identity?.Name ?? "System";
            replyComment.UpdatedBy = _contextAccessor.HttpContext?.User.Identity?.Name ?? "System";

            replyComment.Parent = parentComment;

            await _blogCommentRepository.CreateAsync(replyComment);
            await _blogCommentRepository.SaveChangesAsync();


            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var comment = await _blogCommentRepository.GetAsync(id);

            if (comment is null)
                throw new NotFoundException("BlogComment NotFound");

            var userId = _getUserId();

            if (comment.AppUserId != userId && !_isAdmin())
                throw new UnAuthorizedException("BlogComment NotFound");

           await _blogCommentRepository.Delete(comment);
            await _blogCommentRepository.SaveChangesAsync();
        }

        public async Task<List<BlogCommentGetDto>> GetBlogCommentsAsync(int blogId)
        {
            var blogComments = await _blogCommentRepository.GetFilter(
                              x => x.BlogId == blogId && x.ParentId == null,
                              x => x.Include(x => x.AppUser)
                                   .Include(x => x.Children)
                                      .ThenInclude(child => child.AppUser)
                          ).ToListAsync();
            var dtos = _mapper.Map<List<BlogCommentGetDto>>(blogComments);

            return dtos;
        }

        public Task<List<BlogCommentGetDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BlogCommentGetDto> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

     
        public Task<BlogCommentUpdateDto> GetUpdatedDtoAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(BlogCommentUpdateDto dto, ModelStateDictionary ModelState)
        {
            throw new NotImplementedException();
        }

        private string _getUserId()
        {
            return _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
        }

        private bool _checkAuthorized()
        {
            return _contextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
        }

        private bool _isAdmin()
        {
            return _contextAccessor.HttpContext?.User.IsInRole(IdentityRoles.Admin.ToString()) ?? false;
        }
    }
}
