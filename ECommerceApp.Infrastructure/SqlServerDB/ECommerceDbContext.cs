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
    }
}
