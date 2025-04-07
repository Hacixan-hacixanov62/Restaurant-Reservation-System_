using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Reservation_System_.Service.Services.IService
{
    public interface IOrderService
    {
        Task CancelOrderAsync(int id);
        Task RepairOrderAsync(int id);
        Task NextOrderStatusAsync(int id);
        Task PrevOrderStatusAsync(int id);
    }
}
