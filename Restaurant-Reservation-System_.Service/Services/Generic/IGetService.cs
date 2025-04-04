

using Restaurant_Reservation_System_.Service.Abstractions.Dtos;

namespace Restaurant.BLL.Services.Abstractions.Generics
{
    public interface IGetService<TGetDto>
    where TGetDto : IDto
    {
        Task<TGetDto> GetAsync(int id);
        Task<List<TGetDto>> GetAllAsync();
    }
}
