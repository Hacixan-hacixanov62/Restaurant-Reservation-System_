using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.Service.Profiles;
using System.Reflection;
using Restaurant_Reservation_System_.DataAccess;
using Restaurant_Reservation_System_.Service;
using Dannys.Interceptors;
using Restaurant_Reservation_System_.Service.UI.Services.IService;
using Restaurant_Reservation_System_FinalProject.Services;
using Stripe;
using Restaurant_Reservation_System_FinalProject.Extensions;

namespace Restaurant_Reservation_System_FinalProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration; 


            builder.Services.AddControllersWithViews()
                  .AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );


            builder.Services.AddDalServices(builder.Configuration);
            builder.Services.AddBusinessServices();


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });

            // builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<ILayoutService, LayoutService>();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequiredLength = 6;
                opt.User.RequireUniqueEmail = true;

                // opt.SignIn.RequireConfirmedEmail = true;

                opt.Lockout.MaxFailedAccessAttempts = 3; // cehd sohbeti Remmember !!!
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                opt.Lockout.AllowedForNewUsers = true;


            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("StripeSettings"));

            StripeConfiguration.ApiKey = builder.Configuration["StripeSettings:SecretKey"];


            builder.Services.AddSession(opt =>
            {
                //opt.IdleTimeout = TimeSpan.FromSeconds(20);
            });

            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            builder.Services.AddScoped<BaseEntityInterceptor>();

            builder.Services.AddAutoMapper(opt =>
            {
                opt.AddProfile(new MapperProfiles()); // bu usul bize eger bos constructor yoxdursa onda lazim olacaq
            });


            var app = builder.Build();

            app.UseMiddleware<GlobalExceptionHandler>();


            app.UseRequestLocalization();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Asagdaki kodd setirleri "PipeLine" Adlanir

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                 name: "areas",
                 pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }  
    }
}
