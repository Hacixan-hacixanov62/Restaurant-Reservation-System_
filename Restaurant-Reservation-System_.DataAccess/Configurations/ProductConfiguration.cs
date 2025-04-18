

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant_Reservation_System_.Core.Entittes;

namespace Restaurant_Reservation_System_.DataAccess.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Photos).IsRequired().HasMaxLength(256);

            builder.Property(p => p.Price).IsRequired().HasPrecision(5, 2);
            builder.ToTable(t => t.HasCheckConstraint("CK_Product_Price", "[Price] >= 0"));
        }
    }
}
