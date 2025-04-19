

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant_Reservation_System_.Core.Entittes;

namespace Restaurant_Reservation_System_.DataAccess.Configurations
{
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {         
            builder.Property(x => x.ImageUrl).IsRequired().HasMaxLength(256);
        }
    }
}
