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

        }

        public DatabaseContext()
        {
            var entities = ChangeTracker
                .Entries()
                .Where(x => x.State == EntityState.Modified || x.State == EntityState.Added && x.Entity != null && typeof(BaseModel).IsAssignableFrom(x.Entity.GetType()))
                .ToList();

            var currentTime = DateTime.Now;

            foreach (var entity in entities)
            {
                var entityBase = entity.Entity as BaseModel;

                if (entity.State == EntityState.Added)
                    entityBase.CreatedAt = currentTime;

                entityBase.UpdateAt = currentTime;
            }
        }

        public DbSet<Ship> Ships { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Customer> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
