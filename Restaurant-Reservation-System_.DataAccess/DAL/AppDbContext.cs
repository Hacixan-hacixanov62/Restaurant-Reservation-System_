using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System_.Core.Entittes;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Restaurant_Reservation_System_.DataAccess.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
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
        
        public DbSet<Table> Tables { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;
        public DbSet<Subscribe> Subscribes { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<BlogComment> BlogComments { get; set; } = null!;





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogTopic>().HasKey(x => new { x.BlogId, x.TopicId });
            modelBuilder.Entity<ProductIngredient>().HasKey(x => new { x.ProductId, x.IngredientId }); 
            base.OnModelCreating(modelBuilder);


            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);

        }

         
    }
}


//dotnet ef migrations add InitialMigration --startup-project ..\Restaurant-Reservation-System_FinalProject -o .\DAL\Migrations
//dotnet ef database update --startup-project ..\Restaurant-Reservation-System_FinalProject
