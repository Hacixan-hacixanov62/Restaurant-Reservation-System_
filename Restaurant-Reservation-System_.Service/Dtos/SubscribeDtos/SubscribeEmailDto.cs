using Microsoft.AspNetCore.Http;
using Restaurant_Reservation_System_.Service.Abstractions.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Reservation_System_.Service.Dtos.SubscribeDtos
{
    public class SubscribeEmailDto:IDto
    {
        public string Subject { get; set; } = null!;

        public string Body { get; set; } = null!;

        public List<IFormFile>? Attachments { get; set; } = new();
    }
}
