using AlexisCorePro.Domain.Model;
using System;

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

        public void Seed()
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

            ctx.SaveChanges();
        }
    }
}
