
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant_Reservation_System_.Core.Entittes;

namespace Restaurant_Reservation_System_.DataAccess.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Email)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.ToTable(t => t.HasCheckConstraint("CK_Contact_Email", "Email LIKE '%@%' AND Email LIKE '%.%'"));

            builder.Property(c => c.Phone)
                   .IsRequired()
                   .HasMaxLength(15);

            builder.Property(c => c.Message)
                   .IsRequired()
                   .HasMaxLength(500);
        }
    }
}
