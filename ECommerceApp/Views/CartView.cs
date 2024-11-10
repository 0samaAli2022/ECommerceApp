using ECommerceApp.Application.Interfaces;
using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Views;

public class CartView(ICartService cartService, IOrderService orderService)
{
    private readonly ICartService _cartService = cartService;
    private readonly IOrderService _orderService = orderService;
    public void DisplayCart()
    {
        Cart cart = _cartService.GetCart();
        Clear();
        WriteLine("Cart:");
        WriteLine("--------------");

        if (cart.Items.Count == 0)
        {
            WriteLine("Cart is empty");
            WriteLine("--------------");
            Thread.Sleep(2000);
            return;
        }

        foreach (var item in cart.Items)
        {
            WriteLine($"{item.ProductId}. {item.Product!.Name} x {item.Quantity} - ${item.TotalPrice}");
        }

        WriteLine($"Total: ${cart.TotalPrice}");

        WriteLine("----------------");

        WriteLine("1. Checkout");
        WriteLine("2. Add more items");
        WriteLine("3. Remove an item");

        WriteLine("----------------");
        Write("Enter your choice: ");
        string? choice = ReadLine();
        WriteLine();
        switch (choice)
        {
            case "1":
                _orderService.CreateOrder();
                WriteLine("Order created successfully");
                WriteLine("----------------------------");
                Thread.Sleep(2000);
                break;
            case "2":
                return;
            case "3":
                Write("Enter the product ID to remove: ");

                int productId = int.Parse(ReadLine()!);
                WriteLine();

                if (int.TryParse(productId.ToString(), out productId))
                {
                    try
                    {
                        _cartService.RemoveFromCart(productId);
                        WriteLine($"Product ID: {productId} has been removed from your cart");
                        WriteLine("----------------------------------------------------");
                        WriteLine();
                        Thread.Sleep(1000);
                        DisplayCart();
                    }
                    catch (Exception ex)
                    {
                        WriteLine($"Error: {ex.Message}");
                        WriteLine("--------------------------------------");
                        WriteLine();
                        Thread.Sleep(1000);
                        DisplayCart();
                    }
                }
                break;
            default:
                return;
        }
    }

}
