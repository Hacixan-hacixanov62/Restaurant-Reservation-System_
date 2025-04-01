using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.Service.Dtos.ProductDtos;
using Restaurant_Reservation_System_.Service.Generic;
using Restaurant_Reservation_System_.Service.ViewModels.CategoryVM;
using Restaurant_Reservation_System_.Service.ViewModels.ProductVM;

namespace Restaurant_Reservation_System_.Service.Services.IService
{
    public interface IProductService 
    {
        Task CreateAsync(ProductCreateDto productCreateDto);
        Task DeleteAsync(int id);
        Task<Product> DetailAsync(int id);
        Task<List<Product>> GetAllAsync();
        Task EditAsync(int id, ProductUpdateDto productUpdateDto);

        //Task<ProductCreateDto> GetCreatedDtoAsync();
        //Task<ProductCreateDto> GetCreatedDtoAsync(ProductCreateDto dto);
        //Task<ProductUpdateDto> GetUpdatedDtoAsync(ProductUpdateDto dto);
        Task<bool> IsExistAsync(int id);

    }
}
