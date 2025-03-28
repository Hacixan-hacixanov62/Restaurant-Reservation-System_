using Microsoft.AspNetCore.Mvc.ModelBinding;
using Restaurant_Reservation_System_.Service.Abstractions.Dtos;



namespace Restaurant_Reservation_System_.Service.Generic
{
    public interface IModifyService<TCreateDto, TUpdateDto>
    where TCreateDto : IDto
    where TUpdateDto : IDto
    {
        Task<bool> CreateAsync(TCreateDto dto, ModelStateDictionary ModelState);
        Task<bool> UpdateAsync(TUpdateDto dto, ModelStateDictionary ModelState);
        Task<TUpdateDto> GetUpdatedDtoAsync(int id);
        Task DeleteAsync(int id);
    }
}
