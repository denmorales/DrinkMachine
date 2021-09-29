using System;
using DrinkMachine.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace DrinkMachine.Database
{
    public class DrinkContext : DbContext
    {
        public DbSet<Drink> Drinks {  get; set; }

        public DbSet<DrinkPrice> DrinkPrices {  get; set; }

        public DbSet<Consignment> Consignments {  get; set; }

        public DrinkContext(DbContextOptions<DrinkContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Drink>().HasData(DrinkContextInitializer.InitialDrinks());

            modelBuilder.Entity<DrinkPrice>()
                .HasOne(dp => dp.Drink)
                .WithMany(d => d.Prices)
                .HasForeignKey(dp => dp.DrinkId);

            modelBuilder.Entity<Consignment>()
                .HasOne(dp => dp.Drink)
                .WithMany(d => d.Consignments)
                .HasForeignKey(dp => dp.DrinkId);
        }
    }
}
