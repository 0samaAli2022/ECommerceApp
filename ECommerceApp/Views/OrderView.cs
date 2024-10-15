using ECommerceApp.Models;

namespace ECommerceApp.Views;

public static class OrderView
{

    public static void DisplayOrderHistory(List<Order> orders)
    {
        if (orders.Count == 0)
        {
            WriteLine("No orders found.");
            WriteLine("--------------");
            WriteLine();
            Thread.Sleep(2000);
            return;
        }
        WriteLine("Order History");
        WriteLine("---------------------------------------");

        foreach (var order in orders)
        {
            WriteLine($"{order.OrderId}. {order.OrderDate} - ${order.TotalAmount}");
            WriteLine("---------------------------------------");
            foreach (var item in order.OrderItems)
            {
                WriteLine($"{item.ProductId}. {item.Product.Name} - Quantity: {item.Quantity} - Total Price: ${item.TotalPrice}");
            }
            WriteLine();
        }
        WriteLine("---------------------------------------");

        WriteLine("Press any key to return...");
        ReadKey();
    }
}
