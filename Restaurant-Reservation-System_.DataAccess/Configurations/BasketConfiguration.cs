
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant_Reservation_System_.Core.Entittes;

namespace Restaurant_Reservation_System_.DataAccess.Configurations
{
    public class BasketConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {

            builder.Property(x => x.Count).IsRequired();

            builder.HasIndex(x => new { x.AppUserId, x.ProductId }).IsUnique();
        }
    }
}
