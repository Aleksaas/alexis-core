using AlexisCorePro.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace AlexisCorePro.Domain
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.StateChanged += (object sender, EntityStateChangedEventArgs e) =>
            {
                DateTime currentTime = DateTime.Now;

                BaseModel entityBase = e.Entry.Entity as BaseModel;

                if (e.Entry.State == EntityState.Modified)
                {
                    entityBase.UpdateAt = currentTime;
                    entityBase.UpdatedById = CurrentUserId;
                }
            };

            ChangeTracker.Tracked += (object sender, EntityTrackedEventArgs e) =>
            {
                DateTime currentTime = DateTime.Now;

                BaseModel entityBase = e.Entry.Entity as BaseModel;

                if (e.Entry.State == EntityState.Added)
                {
                    entityBase.CreatedAt = currentTime;
                    entityBase.CreatedById = CurrentUserId;
                    entityBase.UpdateAt = currentTime;
                    entityBase.UpdatedById = CurrentUserId;
                }
            };
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlServer(Startup.Configuration.GetConnectionString("AlexisPro"), optionsAction => optionsAction.EnableRetryOnFailure());
        }

        public int CurrentUserId { get; set; } = 1;

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
