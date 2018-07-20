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
            services.AddScoped<DatabaseContext, DatabaseContext>();
            services.AddScoped<DatabaseInitializer, DatabaseInitializer>();
            services.AddScoped<DatabaseSeed, DatabaseSeed>();
            services.AddScoped<ShipService, ShipService>();
            services.AddScoped<EnumsService, EnumsService>();

            return services;
        }
    }
}
