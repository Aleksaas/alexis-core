using AlexisCorePro.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AlexisCorePro.Infrastructure.Extensions
{
    public static class IWebHostExtensions
    {
        public static IWebHost MigrateDatabase(this IWebHost webHost)
        {
            var serviceScopeFactory = (IServiceScopeFactory)webHost.Services.GetService(typeof(IServiceScopeFactory));

            using (var scope = serviceScopeFactory.CreateScope())
            {
                var services = scope.ServiceProvider;

                var dbContext = services.GetRequiredService<DatabaseContext>();
                var dbInitializer = services.GetRequiredService<DatabaseInitializer>();
                var env = services.GetRequiredService<IHostingEnvironment>();

                if (env.IsDevelopment())
                {
                    dbContext.Database.EnsureDeleted();
                    dbContext.Database.EnsureCreated();
                }

                dbInitializer.Initialize();
            }

            return webHost;
        }
    }
}
