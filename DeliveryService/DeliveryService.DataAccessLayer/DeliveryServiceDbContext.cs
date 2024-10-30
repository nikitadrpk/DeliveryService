using System.Text.Json.Serialization.Metadata;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService;

public class DeliveryServiceDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = deliveryService.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .HasKey(o => o.Id);

        modelBuilder.Entity<Order>()
            .Property(o => o.DeliveryTime)
            .IsRequired();

        modelBuilder.Entity<Order>()
            .Property(o => o.CityDistrict)
            .IsRequired();

        modelBuilder.Entity<Order>()
            .Property(o => o.Weight)
            .IsRequired();
    }
}