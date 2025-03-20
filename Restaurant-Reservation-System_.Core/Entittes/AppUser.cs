using Microsoft.AspNetCore.Identity;

namespace Restaurant_Reservation_System_.Core.Entittes
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}
