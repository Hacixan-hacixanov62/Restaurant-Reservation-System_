
using Restaurant_Reservation_System_.Core.Enums;
using Restaurant_Reservation_System_.Service.Abstractions.Dtos;

namespace Restaurant_Reservation_System_.Service.Generic
{
    public interface IGetServiceWithLanguage<TGetDto>
    where TGetDto : IDto
    {
        Task<TGetDto> GetAsync(int id, Languages language = Languages.Azerbaijan);
        Task<List<TGetDto>> GetAllAsync(Languages language = Languages.Azerbaijan);
    }
}
