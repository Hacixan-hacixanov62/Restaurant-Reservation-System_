using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Restaurant_Reservation_System_.Service.Profiles;
using Restaurant_Reservation_System_.Service.Services;
using Restaurant_Reservation_System_.DataAccess.Repositories.IRepositories;
using Restaurant_Reservation_System_.DataAccess.Repositories;
using Restaurant_Reservation_System_.Service.Services.IService;

namespace Restaurant_Reservation_System_.Service
{
    public static class Restourant_Service_Registration
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            var builder = WebApplication.CreateBuilder();
            var config = builder.Configuration;


            ////builder.Services.AddFluentValidationAutoValidation();
            ////builder.Services.AddFluentValidationClientsideAdapters();
            ////builder.Services.AddValidatorsFromAssemblyContaining<ProductCreateDtoValidator>();

            //builder.Services.AddFluentValidationRulesToSwagger();

            AddServices(services);

            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            builder.Services.AddAutoMapper(opt =>
            {
                opt.AddProfile(new MapperProfiles()); // bu usul bize eger bos constructor yoxdursa onda lazim olacaq
            });



        }

        private static void AddServices(IServiceCollection services)
        {

            services.AddScoped<ICloudinaryService, CloudinaryService>();
         
            //  builder.Services.AddScoped<LayoutService>();
            services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<ISliderRepository, SliderRepository>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IIngridentRepository, IngridientRepository>();
            services.AddScoped<IIngridentService, IngridentService>();

            services.AddScoped<IAboutRepository, AboutRepository>();
            services.AddScoped<IAboutService, AboutService>();

            services.AddScoped<IChefRepository, ChefRepository>();
            services.AddScoped<IChefService, ChefService>();

            services.AddScoped<ITopicRepository, TopicRepository>();
            services.AddScoped<ITopicService, TopicService>();

            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IBlogService, BlogService>();

            services.AddScoped<ITableRepository, TableRepository>();
            services.AddScoped<ITableService, TableService>();

            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IReservationService, ReservationService>();




        }


    }
}
