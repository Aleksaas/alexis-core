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

            return services;
        }
    }
}
