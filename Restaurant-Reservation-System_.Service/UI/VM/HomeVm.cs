using Restaurant_Reservation_System_.Core.Entittes;

namespace Restaurant_Reservation_System_.Service.UI.VM
{
    public class HomeVm
    {
        public List<Slider> Sliders { get; set; }
        public List<About> Abouts { get; set; }
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
        //public List<BlogComment> BlogComments { get; set; } = new List<BlogComment> { };


    }
}
    