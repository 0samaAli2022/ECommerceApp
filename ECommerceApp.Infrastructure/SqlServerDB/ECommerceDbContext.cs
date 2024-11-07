using ECommerceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Infrastructure.SqlServerDB;

public class ECommerceDbContext : DbContext
{
    public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
             .HasOne(u => u.ShoppingCart)
             .WithOne(c => c.User)
             .HasForeignKey<Cart>(c => c.UserId)
             .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CartItem>()
                 .HasOne(c => c.Product)
                 .WithMany(p => p.CartItems)
                 .HasForeignKey(c => c.ProductId)
                 .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CartItem>()
            .HasOne(c => c.Cart)
            .WithMany(c => c.Items)
            .HasForeignKey(c => c.CartId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderItem>()
                 .HasOne(o => o.Product)
                 .WithMany(p => p.OrderItems)
                 .HasForeignKey(o => o.ProductId)
                 .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Order>()
                 .HasOne(o => o.User)
                 .WithMany(u => u.Orders)
                 .HasForeignKey(o => o.UserId)
                 .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderItem>()
                 .HasOne(o => o.Order)
                 .WithMany(u => u.OrderItems)
                 .HasForeignKey(o => o.OrderId)
                 .OnDelete(DeleteBehavior.Cascade);

        SeedUsers(modelBuilder);
        SeedProducts(modelBuilder);

    }
    private void SeedProducts(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Name = "Laptop",
                Description = "High performance laptop",
                Price = 999.99m,
                StockQuantity = 50,
                CreatedBy = 1
            },
            new Product
            {
                Id = 2,
                Name = "Smartphone",
                Description = "Latest model smartphone",
                Price = 699.99m,
                StockQuantity = 100,
                CreatedBy = 1
            },
            new Product
            {
                Id = 3,
                Name = "Headphones",
                Description = "Noise-cancelling headphones",
                Price = 199.99m,
                StockQuantity = 200,
                CreatedBy = 1
            },
            new Product
            {
                Id = 4,
                Name = "Smartwatch",
                Description = "Feature-rich smartwatch",
                Price = 299.99m,
                StockQuantity = 75,
                CreatedBy = 1
            },
            new Product
            {
                Id = 5,
                Name = "Tablet",
                Description = "Lightweight and powerful tablet",
                Price = 399.99m,
                StockQuantity = 150,
                CreatedBy = 1
            }
        );
    }
    private void SeedUsers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Username = "admin",
                Password = "admin",
                Email = "admin",
                CreatedBy = 1
            },
            new User
            {
                Id = 2,
                Username = "osama",
                Password = "asd",
                Email = "asd",
                CreatedBy = 2
            }
        );
    }
}
