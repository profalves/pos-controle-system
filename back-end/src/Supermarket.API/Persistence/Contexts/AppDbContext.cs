using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.ValueGeneration.Internal;
using Supermarket.API.Domain.Models;

namespace Supermarket.API.Persistence.Contexts
{
  public class AppDbContext : DbContext
  {
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      builder.Entity<Category>().ToTable("Categories");
      builder.Entity<Category>().HasKey(p => p.Id);
      builder.Entity<Category>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();//.HasValueGenerator<InMemoryIntegerValueGenerator<int>>();
      builder.Entity<Category>().Property(p => p.Name).IsRequired().HasMaxLength(30);
      builder.Entity<Category>().HasMany(p => p.Products).WithOne(p => p.Category).HasForeignKey(p => p.CategoryId);

      builder.Entity<Category>().HasData
      (
          new Category { Id = 100, Name = "Hort-Fruit" }, // Id set manually due to in-memory provider
          new Category { Id = 101, Name = "Laticínios" },
          new Category { Id = 102, Name = "Outros" }
      );

      builder.Entity<Product>().ToTable("Products");
      builder.Entity<Product>().HasKey(p => p.Id);
      builder.Entity<Product>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
      builder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(50);
      builder.Entity<Product>().Property(p => p.QuantityInPackage).IsRequired();
      builder.Entity<Product>().Property(p => p.UnitOfMeasurement).IsRequired();
      builder.Entity<Product>().Property(p => p.Price).IsRequired();

      builder.Entity<Product>().HasData
      (
          new Product
          {
            Id = 100,
            Name = "Maçã",
            QuantityInPackage = 1,
            UnitOfMeasurement = EUnitOfMeasurement.Unity,
            CategoryId = 100,
            Price = 2
          },
          new Product
          {
            Id = 101,
            Name = "Leite",
            QuantityInPackage = 2,
            UnitOfMeasurement = EUnitOfMeasurement.Liter,
            CategoryId = 101,
            Price = 5
          }
      );
    }
  }
}