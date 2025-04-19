
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant_Reservation_System_.Core.Entittes;

namespace Restaurant_Reservation_System_.DataAccess.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(256);
            builder.Property(x => x.Date).IsRequired();
           // builder.ToTable(t => t.HasCheckConstraint("CK_Reservation_People_Range", "[NumberOfPeople] >= 1 AND [NumberOfPeople] <= 10"));


            builder.Property(c => c.Email)
                  .IsRequired()
                  .HasMaxLength(50);

        }
    }
}
