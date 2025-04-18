

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant_Reservation_System_.Core.Entittes;

namespace Restaurant_Reservation_System_.DataAccess.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.Property(x => x.Url).IsRequired().HasMaxLength(256);
            builder.Property(x => x.IsMain).HasDefaultValue(false);

            builder.HasIndex(x => new { x.ProductId, x.IsMain })
                       .IsUnique()
                       .HasFilter("[IsMain] = 1");
        }
    }
}
