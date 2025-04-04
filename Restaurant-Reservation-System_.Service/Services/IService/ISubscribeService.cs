using Microsoft.AspNetCore.Mvc.ModelBinding;
using Restaurant.BLL.Services.Abstractions.Generics;
using Restaurant_Reservation_System_.Service.Dtos.SubscribeDtos;
using Restaurant_Reservation_System_.Service.Generic;

namespace Restaurant_Reservation_System_.Service.Services.IService
{
    public interface ISubscribeService : IModifyService<SubscribeCreateDto, SubscribeUpdateDto>, IGetService<SubscribeGetDto>
    {
        Task<bool> SendEmailToSubscribres(SubscribeEmailDto dto, ModelStateDictionary ModelState);
    }
}
