
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.Core.Enums;
using Restaurant_Reservation_System_.DataAccess.Localizers;
using Restaurant_Reservation_System_.DataAccess.Repositories;
using Restaurant_Reservation_System_.DataAccess.Repositories.IRepositories;
using Restaurant_Reservation_System_.Service.Dtos.CommentDtos;
using Restaurant_Reservation_System_.Service.Exceptions;
using Restaurant_Reservation_System_.Service.Services.IService;
using System.Security.Claims;

namespace Restaurant_Reservation_System_.Service.Services
{
    public class CommentService:ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;

        public CommentService(ICommentRepository commentRepository, IMapper mapper, IHttpContextAccessor contextAccessor, IProductService productService, IOrderService orderService)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _productService = productService;
            _orderService = orderService;
        }

        public async Task<bool> CheckIsAllowCommentAsync(int productId)
        {

            var orders = await _orderService.GetAllAsync();

            var userId = _getUserId();


            var isExist = await _commentRepository.IsExistAsync(x => x.ProductId == productId && x.AppUserId == userId);

            if (isExist)
                return false;

            return true;
        }

        public async Task<bool> CreateAsync(CommentCreateDto dto, ModelStateDictionary ModelState)
        {
            if (!ModelState.IsValid)
                return false;

            if (!_checkAuthorized())
                throw new UnAuthorizedException("NotFound Comment");

            var orders = await _orderService.GetAllAsync();

            var userId = _getUserId();

            var comment = _mapper.Map<Comment>(dto);

            comment.AppUserId = userId;
            comment.CreatedBy = _contextAccessor.HttpContext?.User.Identity?.Name ?? "System"; // İstifadəçi adı və ya "System"
            comment.UpdatedBy = _contextAccessor.HttpContext?.User.Identity?.Name ?? "System"; // İstifadəçi adı və ya "System"


            await _commentRepository.CreateAsync(comment);
            await _commentRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CreateReplyAsync(CommentReplyDto dto, ModelStateDictionary ModelState)
        {
            if (!ModelState.IsValid)
                return false;

            if (!_checkAuthorized())
                throw new UnAuthorizedException("NotFound Comment");


            var orders = await _orderService.GetAllAsync();

            var userId = _getUserId();


            var parentComment = await _commentRepository.GetAsync(dto.ParentId);
            if (parentComment == null) throw new NotFoundException("Parent comment not found");

            var replyComment = _mapper.Map<Comment>(dto);
            replyComment.AppUserId = userId;
            replyComment.CreatedBy = _contextAccessor.HttpContext?.User.Identity?.Name ?? "System";
            replyComment.UpdatedBy = _contextAccessor.HttpContext?.User.Identity?.Name ?? "System";

            replyComment.Parent = parentComment;

            await _commentRepository.CreateAsync(replyComment);
            await _commentRepository.SaveChangesAsync();


            return true;
        }

        public async Task DeleteAsync(int id)
        {

            var comment = await _commentRepository.GetAsync(c => c.Id == id, include: q => q.Include(c => c.Children));

            if (comment is null)
                throw new NotFoundException("NotFound Comment");

            var userId = _getUserId();

            if (comment.AppUserId != userId && !_isAdmin())
                throw new UnAuthorizedException("NotFound Comment");

            await DeleteChildCommentsAsync(comment);



            _commentRepository.Delete(comment);
            await _commentRepository.SaveChangesAsync();
        }


        private async Task DeleteChildCommentsAsync(Comment parentComment)
        {
            if (parentComment.Children != null && parentComment.Children.Any())
            {
                foreach (var child in parentComment.Children.ToList()) // Şərhləri təkrarlayın
                {
                    await DeleteChildCommentsAsync(child); // Rekursiv olaraq bağlı şərhləri silin
                    _commentRepository.Delete(child); // Şərhi silin
                }
            }
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

        public async Task<List<CommentGetDto>> GetProductCommentsAsync(int productId)
        {
            var comments = await _commentRepository.GetFilter(
                   x => x.ProductId == productId && x.ParentId == null,
                   x => x.Include(x => x.AppUser)
                        .Include(x => x.Children)
                           .ThenInclude(child => child.AppUser)
               ).ToListAsync();
            var dtos = _mapper.Map<List<CommentGetDto>>(comments);

            return dtos;
        }

        public Task<CommentUpdateDto> GetUpdatedDtoAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(CommentUpdateDto dto, ModelStateDictionary ModelState)
        {
            throw new NotImplementedException();
        }

        public Task<List<CommentGetDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CommentGetDto> GetAsync(int id)
        {
            throw new NotImplementedException();
        }






    }
}
