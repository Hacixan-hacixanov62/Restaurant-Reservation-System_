using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.Core.Enums;
using Restaurant_Reservation_System_.Service.Dtos.CategoryDtos;
using Restaurant_Reservation_System_.Service.Generic;
using Restaurant_Reservation_System_.Service.ViewModels.CategoryVM;
using Restaurant_Reservation_System_.Service.ViewModels.SliderVM;

namespace Restaurant_Reservation_System_.Service.Services.IService
{
    public interface ICategoryService
    {
        Task CreateAsync(CategoryCreateDto categoryCreateDto);
        Task DeleteAsync(int id);
        Task<Category> DetailAsync(int id);
        Task<List<Category>> GetAllAsync();
        Task EditAsync(int id, CategoryUpdateDto categoryUpdateDto);
        //Task<List<CategoryGetDto>> GetCategoriesAsync(Languages language = Languages.Azerbaijan);

        //Task<bool> IsExistAsync(int id);
        //Task<CategoryCreateDto> GetCreateDtoAsync();
        //Task<CategoryCreateDto> GetCreateDtoAsync(CategoryCreateDto dto);
        //Task<CategoryUpdateDto> GetUpdatedDtoAsync(CategoryUpdateDto dto);
    }
}
