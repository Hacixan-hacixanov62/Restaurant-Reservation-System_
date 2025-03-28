using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant_Reservation_System_.DataAccess.Localizers;

namespace Restaurant_Reservation_System_.DataAccess
{
    public static class DataAcceseeLayerService_Registration
    {
        public static IServiceCollection AddDalServices(this IServiceCollection services, IConfiguration configuration)
        {
            _addLocalizers(services);

            return services;
        }

        private static void _addLocalizers(IServiceCollection services)
        {

         
          

        }

    }
}
