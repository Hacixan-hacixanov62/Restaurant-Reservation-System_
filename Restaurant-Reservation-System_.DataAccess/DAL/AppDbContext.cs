using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System_.Core.Entittes;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Dannys.Interceptors;
using Restaurant_Reservation_System_.DataAccess.Configurations;

namespace Restaurant_Reservation_System_.DataAccess.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        private readonly BaseEntityInterceptor _interceptor;


        public AppDbContext(DbContextOptions<AppDbContext> options, BaseEntityInterceptor interceptor) : base(options)
        {
            _interceptor = interceptor;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.AddInterceptors(_interceptor);
        }


        public DbSet<Slider> Sliders { get; set; } = null!;
        public DbSet<About> Abouts { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<CategoryDetail> CategoryDetails { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<ProductImage> ProductImages { get; set; } = null!;
        public DbSet<ProductDetail> ProductDetails { get; set; } = null!;
        public DbSet<Ingredient> Ingredients { get; set; } = null!;
        public DbSet<ProductIngredient> ProductIngredients { get; set; } = null!;
        public DbSet<Language> Languages { get; set; } = null!;
        public DbSet<Chef> Chefs { get; set; } = null!;
        public DbSet<Blog> Blogs { get; set; } = null!;
        public DbSet<Topic> Topics { get; set; } = null!;
        public DbSet<BlogTopic> BlogTopics { get; set; } = null!;
        public DbSet<Contact> Contacts { get; set; } = null!;
        public DbSet<Table> Tables { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;
        public DbSet<Subscribe> Subscribes { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<BlogComment> BlogComments { get; set; } = null!;
        public DbSet<CartItem> CartItems { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;

        // SignalR Chat
        public DbSet<Message> Messages  { get; set; } = null!;
        public DbSet<Chat> Chats { get; set; } = null!;
        public DbSet<AppUserChat> AppUserChats { get; set; } = null!;



        // Message(SignalR) hele teze baslamisam Migration elemek olmur buna bax !!!!
        // Reservation Tablede de men CartServicede Methoda Products error verir neye gore Collectondan product istediyne gore oradada migrationa gore problem var  ona da Bax !!!

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BlogTopic>().HasKey(x => new { x.BlogId, x.TopicId });
            modelBuilder.Entity<ProductIngredient>().HasKey(x => new { x.ProductId, x.IngredientId });
            modelBuilder.Entity<Comment>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Product>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Blog>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<BlogComment>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Category>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Chat>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Chef>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Message>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Reservation>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Topic>().HasQueryFilter(x => !x.IsDeleted);

            // HATAYI ÖNLER

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CommentConfiguration).Assembly);

            //foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            //{
            //    relationship.DeleteBehavior = DeleteBehavior.NoAction;
            //}
            //modelBuilder.Entity<Product>()
            //    .HasMany<Comment>(c => c.Comments)
            //    .WithOptional(x => x.Product)
            //    .WillCascadeOnDelete(true);

        }



    }
}




//dotnet ef migrations add InitialMigration --startup-project ..\Restaurant-Reservation-System_FinalProject -o .\DAL\Migrationsc
//dotnet ef database update --startup-project ..\Restaurant-Reservation-System_FinalProject
//dotnet ef migrations remove --startup-project ..\Restaurant-Reservation-System_FinalProject