using AlexisCorePro.Business.Common;
using AlexisCorePro.Business.Ships;
using AlexisCorePro.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace AlexisCorePro.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(
            this IServiceCollection services)
        {
            services.AddTransient<DatabaseContext, DatabaseContext>();
            services.AddTransient<DatabaseInitializer, DatabaseInitializer>();
            services.AddTransient<DatabaseSeed, DatabaseSeed>();
            services.AddTransient<ShipService, ShipService>();
            services.AddTransient<EnumsService, EnumsService>();

            return services;
        }
    }
}
