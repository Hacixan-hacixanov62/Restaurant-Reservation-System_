
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Restaurant.BLL.Services.Abstractions.Generics;
using Restaurant_Reservation_System_.Service.Dtos.CommentDtos;
using Restaurant_Reservation_System_.Service.Generic;

namespace Restaurant_Reservation_System_.Service.Services.IService
{
    public interface ICommentService : IModifyService<CommentCreateDto, CommentUpdateDto>, IGetService<CommentGetDto>
    {
        Task<List<CommentGetDto>> GetProductCommentsAsync(int productId);
        Task<bool> CheckIsAllowCommentAsync(int productId);

        Task<bool> CreateReplyAsync(CommentReplyDto dto, ModelStateDictionary ModelState);
    }
}
