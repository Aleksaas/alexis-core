using AlexisCorePro.Domain;
using AlexisCorePro.Domain.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace AlexisCorePro.Infrastructure.Extensions
{
    public static class IWebHostExtensions
    {
        public static async Task<IApplicationBuilder> MigrateDatabase(this IApplicationBuilder webHost)
        {
            var serviceScopeFactory = (IServiceScopeFactory)webHost.ApplicationServices.GetService(typeof(IServiceScopeFactory));

            using (var scope = serviceScopeFactory.CreateScope())
            {
                var services = scope.ServiceProvider;

                var dbContext = services.GetRequiredService<DatabaseContext>();
                var userManager = services.GetRequiredService<UserManager<User>>();
                var dbInitializer = services.GetRequiredService<DatabaseInitializer>();
                var dbSeed = services.GetRequiredService<DatabaseSeed>();
                var env = services.GetRequiredService<IHostingEnvironment>();

                if (env.IsDevelopment())
                {
                    dbContext.Database.EnsureDeleted();
                    dbContext.Database.EnsureCreated();
                    dbInitializer.Initialize();
                    await dbSeed.Seed(userManager);
                }
                else
                {
                    dbContext.Database.Migrate();
                    dbInitializer.Initialize();
                }

            }

            return webHost;
        }
    }
}
