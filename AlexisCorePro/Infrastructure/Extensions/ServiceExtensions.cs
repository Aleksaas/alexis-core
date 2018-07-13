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

            return services;
        }
    }
}
