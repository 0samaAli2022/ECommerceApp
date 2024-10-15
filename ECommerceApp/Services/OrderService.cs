
using ECommerceApp.Data;
using ECommerceApp.Models;

namespace ECommerceApp.Services;

public class OrderService(AuthService authService)
{
    private readonly AuthService _authService = authService;
    public static void CreateOrder(ShoppingCart shoppingCart)
    {
        // Save order to database
        Order order = new()
        {
            OrderId = Database.Orders.Count + 1,
            UserId = shoppingCart.UserId,
            TotalAmount = shoppingCart.TotalPrice,
            OrderItems = shoppingCart.Items.Select(item => new CartItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                TotalPrice = item.TotalPrice,
                Product = item.Product
            }).ToList(), // Create a new list of OrderItem objects
            OrderDate = DateTime.Now
        };

        Database.Orders.Add(order);

        // Clear shopping cart
        shoppingCart.Items.Clear();
    }
    public List<Order> GetAll()
    {
        return Database.Orders.Where(o => o.UserId == _authService.CurrentUser!.UserId).ToList();
    }
}
