using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.Service.Dtos.TableDtos;

namespace Restaurant_Reservation_System_.Service.Services.IService
{
    public interface ITableService
    {
        Task CreateAsync(TableCreateDto tableCreateDto);
        Task DeleteAsync(int id);
        Task<Table> DetailAsync(int id);
        Task<List<Table>> GetAllAsync();
        Task EditAsync(int id, TableUpdateDto tableUpdateDto);
    }
}
