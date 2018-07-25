using AlexisCorePro.Domain.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AlexisCorePro.Domain
{
    /// <summary>
    /// Populets db with some data for testing
    /// </summary>
    public class DatabaseSeed
    {
        private DatabaseContext ctx;

        public DatabaseSeed(DatabaseContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task Seed(UserManager<User> userManager)
        {
            var company1 = ctx.Companies.Add(new Company
            {
                Name = "Company #111"
            }).Entity;

            var customer1 = ctx.Customers.Add(new Customer
            {
                Name = "Customer #111",
                Company = company1
            }).Entity;

            var ship1 = ctx.Ships.Add(new Ship
            {
                Name = "Ship #111",
                Mmsi = 5,
                Imd = 10,
                Customer = customer1
            }).Entity;

            var equipment1 = ctx.Equipments.Add(new Equipment
            {
                Name = "Equipment-#1",
                Ship = ship1
            });

            IdentityResult result = await userManager.CreateAsync(new User
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",

            }, "Admin123!");

            var adminUser = ctx.Users.First(u => u.Email == "admin@gmail.com");
            var adminRole = ctx.Roles.First(u => u.Name == Roles.Admin);

            ctx.UserRoles.Add(new UserRole
            {
                UserId = adminUser.Id,
                RoleId = adminRole.Id
            });

            ctx.SaveChanges();
        }
    }
}
