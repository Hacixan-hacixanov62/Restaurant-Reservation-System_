
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant_Reservation_System_.Core.Entittes;
using System.Reflection.Emit;

namespace Restaurant_Reservation_System_.DataAccess.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(x => x.Text).IsRequired().HasMaxLength(256);
            builder.Property(x => x.Rating).IsRequired();

            builder.ToTable(t => t.HasCheckConstraint("CK_Comment_Rating_Range", "[Rating] >= 0 AND [Rating] <= 5"));


            builder.HasOne(c => c.Parent)
                .WithMany(c => c.Children)
                .HasForeignKey(c => c.ParentId);
               
        }
    }
}
