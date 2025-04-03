using Restaurant_Reservation_System_.Core.Entittes;

namespace Restaurant_Reservation_System_.Service.UI.VM
{
    public class BlogVM
    {
        public List<Blog> Blogs { get; set; } = new();
        public List<Topic> Topics { get; set; } = new();
    }


    public class BlogDetailVM
    {
        public Blog Blog { get; set; } = new();
        public List<Topic> Topics { get; set; } = new();
        public Blog? PrevBlog { get; set; }
        public Blog? NextBlog { get; set; }
    }


}
