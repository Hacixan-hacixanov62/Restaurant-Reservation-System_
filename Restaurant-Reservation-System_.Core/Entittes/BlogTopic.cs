namespace Restaurant_Reservation_System_.Core.Entittes
{
    public class BlogTopic
    {
        public int BlogId { get; set; }
        public Blog Blog { get; set; } = null!;
        public int TopicId { get; set; }
        public Topic Topic { get; set; } = null!;
    }
}
