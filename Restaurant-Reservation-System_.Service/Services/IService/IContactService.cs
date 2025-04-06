
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Restaurant_Reservation_System_.Service.UI.Dtos;

namespace Restaurant_Reservation_System_.Service.Services.IService
{
    public interface IContactService
    {
        Task<bool> SendEmailAsync(ContactDto dto, ModelStateDictionary ModelState);
    }
}
    