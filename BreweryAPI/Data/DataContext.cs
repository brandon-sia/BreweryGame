using BreweryAPI.Models;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;

namespace BreweryAPI.Data
{
    public class DataContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;
        public DataContext(DbContextOptions<DataContext> options, ILoggerFactory loggerFactory) : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory); // Configure EF Core to use logger factory
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Beer> Beers { get; set; }
        public DbSet<Brewery> Breweries { get; set; }
        public DbSet<Wholesaler> Wholesalers { get; set; }
        public DbSet<Inventory> Inventories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Beer>()
               .HasOne(b => b.Brewer)
               .WithMany(br => br.Beers)
               .HasForeignKey(b => b.BrewerId);

            // Wholesaler to Inventory relationship
            modelBuilder.Entity<Inventory>()
                .HasOne(i => i.Wholesaler)
                .WithMany(w => w.Inventories)
                .HasForeignKey(i => i.WholesalerId);

            // Beer to Inventory relationship
            modelBuilder.Entity<Inventory>()
                .HasOne(i => i.Beer)
                .WithMany(b => b.Inventories)
                .HasForeignKey(i => i.BeerId);

            // Optional: Enforce unique inventory for Wholesaler and Beer
            modelBuilder.Entity<Inventory>()
                .HasIndex(i => new { i.WholesalerId, i.BeerId })
                .IsUnique();
        }

    }
}
