using ECommerceApp.Models;

namespace ECommerceApp.Views;


public static class ProductView
{
    public static void DisplayProducts(List<Product> products)
    {
        WriteLine("Products Available:");
        WriteLine("------------------");

        foreach (var product in products)
        {
            WriteLine($"{product.ProductId}. {product.Name} - ${product.Price}");
        }
        WriteLine("------------------");
    }

    public static int SelectProduct()
    {
        Write("Enter the Product ID to select a product or 0 to return to main menu: ");
        string? input = ReadLine();
        WriteLine();
        if (int.TryParse(input, out int productId))
        {
            return productId;
        }

        WriteLine("Invalid input. Try again.");
        return -1; // Return -1 if invalid
    }
}


