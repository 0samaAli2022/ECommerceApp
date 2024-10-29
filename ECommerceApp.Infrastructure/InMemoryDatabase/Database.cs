
using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Infrastructure.InMemoryDatabase;

public static class Database
{
    internal static List<Product> Products { get; set; } = [];
    internal static List<User> Users { get; set; } = [];
    internal static List<ShoppingCart> ShoppingCarts { get; set; } = [];
    internal static List<Order> Orders { get; set; } = [];
}
