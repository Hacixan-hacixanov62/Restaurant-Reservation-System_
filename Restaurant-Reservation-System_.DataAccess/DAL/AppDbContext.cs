using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System_.Core.Entittes;

namespace Restaurant_Reservation_System_.DataAccess.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Slider> Sliders { get; set; }
    }
}


//dotnet ef migrations add InitialMigration --startup-project ..\Restaurant-Reservation-System_FinalProject -o .\DAL\Migrations
//dotnet ef database update --startup-project ..\Restaurant-Reservation-System_FinalProject