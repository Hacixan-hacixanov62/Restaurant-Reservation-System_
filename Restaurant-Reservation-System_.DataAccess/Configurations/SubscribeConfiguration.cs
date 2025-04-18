

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant_Reservation_System_.Core.Entittes;

namespace Restaurant_Reservation_System_.DataAccess.Configurations
{
    public class SubscribeConfiguration : IEntityTypeConfiguration<Subscribe>
    {
        public void Configure(EntityTypeBuilder<Subscribe> builder)
        {
            builder.Property(x => x.IsSubscribed).HasDefaultValue(false);

            builder.Property(c => c.Email)
                  .IsRequired()
                  .HasMaxLength(255);

            builder.ToTable(t => t.HasCheckConstraint("CK_Contact_Email", "Email LIKE '%@%' AND Email LIKE '%.%'"));

        }
    }
}
