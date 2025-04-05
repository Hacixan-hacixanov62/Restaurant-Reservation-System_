using Microsoft.AspNetCore.Mvc.ModelBinding;
using Restaurant.BLL.Services.Abstractions.Generics;
using Restaurant_Reservation_System_.Service.Dtos.BlogCommentDtos;
using Restaurant_Reservation_System_.Service.Generic;


namespace Restaurant_Reservation_System_.Service.Services.IService
{
    public interface IBlogCommentService:IModifyService<BlogCommentCreateDto,BlogCommentUpdateDto> , IGetService<BlogCommentGetDto>
    {
        Task<List<BlogCommentGetDto>> GetBlogCommentsAsync(int blogId);
        Task<bool> CheckIsAllowBlogCommentAsync(int blogId);
        Task<bool> CreateReplyAsync(BlogCommentReplyDto dto, ModelStateDictionary ModelState);
    }
}
