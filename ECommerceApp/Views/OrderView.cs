using ECommerceApp.Application.Interfaces;

namespace ECommerceApp.Views;

public class OrderView(IOrderService orderService)
{
    private readonly IOrderService _orderService = orderService;

    public void DisplayOrderHistory()
    {
        var orders = _orderService.GetAll();
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
            WriteLine($"{order.Id}. {order.OrderDate} - ${order.TotalAmount}");
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
