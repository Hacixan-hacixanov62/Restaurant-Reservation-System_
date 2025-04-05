

using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.DataAccess.DAL;

namespace Restaurant_Reservation_System_.DataAccess.Repositories
{
    public class BlogCommentRepository : Repository<BlogComment>
    {
        public BlogCommentRepository(AppDbContext context) : base(context)
        {
        }
    }
}
