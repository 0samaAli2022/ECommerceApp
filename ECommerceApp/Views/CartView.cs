
using ECommerceApp.Models;
using ECommerceApp.Services;

namespace ECommerceApp.Views;

public class CartView
{
    public static void DisplayCart(ShoppingCart cart)
    {
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
            WriteLine($"{item.ProductId}. {item.Product.Name} x {item.Quantity} - ${item.TotalPrice}");
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
                OrderService.CreateOrder(cart);
                WriteLine("Order created successfully");
                WriteLine("----------------------------");
                Thread.Sleep(2000);
                break;
            case "2":
                return;
            case "3":
                Write("Enter the product ID to remove: ");

                int? productId = int.Parse(ReadLine());
                WriteLine();
                if (productId != null)
                {
                    var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
                    if (item != null)
                    {
                        item.Quantity -= 1;
                        item.TotalPrice -= item.Product.Price;
                        if (item.Quantity <= 0)
                        {
                            cart.Items.Remove(item);
                        }
                        WriteLine($"Product ID: {productId} has been removed from your cart");
                        WriteLine("------------------------------------------------");
                        WriteLine();
                        Thread.Sleep(1000);
                        DisplayCart(cart);
                    }
                    else
                    {
                        WriteLine($"Product ID: {productId} not found in your cart");
                        WriteLine("--------------------------------------");
                        WriteLine();
                        Thread.Sleep(1000);
                    }
                }
                break;
            default:
                return;
        }
    }

}
