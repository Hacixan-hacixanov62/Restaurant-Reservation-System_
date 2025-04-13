using Microsoft.AspNetCore.Mvc.ModelBinding;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.Service.Dtos.ReservationDtos;

namespace Restaurant_Reservation_System_.Service.Services.IService
{
    public interface IReservationService
    {

        Task<bool> CreateAsync(ReservationCreateDto dto, ModelStateDictionary ModelState);
        //Task<ReservationDto> GetReservationAsync(int id);
        //Task<ReservationDto?> GetLatestReservationAsync(string name, string phoneNumber);
        Task<Reservation> RepairOrEnd(int id);
    }
}
