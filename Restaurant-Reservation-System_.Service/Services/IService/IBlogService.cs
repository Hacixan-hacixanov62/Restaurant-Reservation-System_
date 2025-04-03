
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.Service.Dtos.BlogDtos;
using Restaurant_Reservation_System_.Service.Dtos.ProductDtos;

namespace Restaurant_Reservation_System_.Service.Services.IService
{
    public interface IBlogService
    {
        Task CreateAsync(BlogCreateDto blogCreateDto);
        Task DeleteAsync(int id);
        Task<Blog> DetailAsync(int id);
        Task<List<Blog>> GetAllAsync();
        Task EditAsync(int id, BlogUpdateDto blogUpdateDto);
        Task<bool> IsExistAsync(int id);
    }
}
