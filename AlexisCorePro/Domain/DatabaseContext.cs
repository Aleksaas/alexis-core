using AlexisCorePro.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AlexisCorePro.Domain
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
            //var entities = ChangeTracker
            //    .Entries()
            //    .Where(x => x.State == EntityState.Modified || x.State == EntityState.Added && x.Entity != null && typeof(BaseModel).IsAssignableFrom(x.Entity.GetType()))
            //    .ToList();

            //var currentTime = DateTime.Now;

            //foreach (var entity in entities)
            //{
            //    var entityBase = entity.Entity as BaseModel;

            //    if (entity.State == EntityState.Added)
            //        entityBase.CreatedAt = currentTime;

            //    entityBase.UpdateAt = currentTime;
            //}
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlServer(Startup.Configuration.GetConnectionString("AlexisPro"), optionsAction => optionsAction.EnableRetryOnFailure());
        }

        public DbSet<Ship> Ships { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Equipment> Equipments { get; set; }

        public DbSet<Postnumber> Postnumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // modelBuilder.Entity<Ship>().HasData(new Ship { Id = 1, Name = "ShipTesting1" });
            // modelBuilder.Entity<Ship>().HasQueryFilter(e => e.Id > 10);
        }
    }
}
