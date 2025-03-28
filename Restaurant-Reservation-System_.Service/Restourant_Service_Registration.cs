using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Restaurant_Reservation_System_.Service.Validators.CategoryValidators;
using System.Reflection;
using Restaurant_Reservation_System_.Service.Profiles;
using Restaurant_Reservation_System_.Service.Validators.ProductValidators;

namespace Restaurant_Reservation_System_.Service
{
    public class Restourant_Service_Registration
    {
        public static void AddBusinessServices(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;


            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssemblyContaining<ProductCreateDtoValidator>();

            //builder.Services.AddFluentValidationRulesToSwagger();

            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            builder.Services.AddAutoMapper(opt =>
            {
                opt.AddProfile(new MapperProfiles()); // bu usul bize eger bos constructor yoxdursa onda lazim olacaq
            });




        }
    }
}
