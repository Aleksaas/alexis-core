using AlexisCorePro.Domain.Model;

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
                Name = "Company-#1"
            }).Entity;

            var customer1 = ctx.Customers.Add(new Customer
            {
                Name = "Customer-#1",
                Company = company1
            }).Entity;

            var ship1 = ctx.Ships.Add(new Ship
            {
                Name = "Ship-#1",
                Mmsi = 5,
                Imd = 10,
                Customer = customer1
            }).Entity;

            ctx.SaveChanges();
        }
    }
}
