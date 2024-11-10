using ECommerceApp.Application.Interfaces;
using ECommerceApp.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace ECommerceApp.Views;


public class ProductView(IProductService productService, ILogger<ProductView> logger)
{
    private readonly IProductService _productService = productService;
    private readonly ILogger<ProductView> _logger = logger;

    public void DisplayProducts()
    {
        var products = _productService.GetAll();
        WriteLine("Products Available:");
        WriteLine("--------------------------");

        foreach (var product in products)
        {
            WriteLine($"{product.Id}. {product.Name} - ${product.Price} - {product.StockQuantity} in stock");
        }
        WriteLine("--------------------------");
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
        Thread.Sleep(2000);
        return -1; // Return -1 if invalid
    }

    public void AddProduct()
    {
        WriteLine("Add New Product");
        WriteLine("--------------------");
        Write("Name: ");
        string? name = ReadLine();

        Write("Description: ");
        string? description = ReadLine();

        Write("Price: ");
        string? priceInput = ReadLine();

        if (!decimal.TryParse(priceInput, out decimal price))
        {
            WriteLine("Invalid input. Try again.");
            Thread.Sleep(2000);
            return;
        }

        Write("Quantity: ");
        string? quantityInput = ReadLine();


        if (!int.TryParse(quantityInput, out int quantity))
        {
            WriteLine("Invalid input. Try again.");
            Thread.Sleep(2000);
            return;
        }
        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(description) && price > 0 && quantity > 0)
        {
            var product = new Product()
            {
                Name = name,
                Description = description,
                Price = price,
                StockQuantity = quantity,
                CreatedBy = 1
            };

            _productService.Add(product);
            _logger.LogInformation("Product added: {Name} By UserID: {CreatedBy}", name, product.CreatedBy);
            WriteLine("Product added successfully.");
            Thread.Sleep(2000);
            return;
        }

        WriteLine("Invalid input. Try again.");
        Thread.Sleep(2000);
        AddProduct();
    }

    public void RemoveProduct()
    {
        WriteLine("Remove Product");
        WriteLine("--------------------");

        Write("Product ID: ");
        string? input = ReadLine();
        WriteLine();
        // Try to parse the input to an integer
        if (!int.TryParse(input, out int productId))
        {
            WriteLine("Invalid input. Please enter a valid product ID.");
            Thread.Sleep(2000);
            RemoveProduct(); // Call the method again to allow the user to retry
        }

        Product? product = _productService.GetById(productId);
        if (product == null)
        {
            WriteLine("There is no product with that ID.");
            Thread.Sleep(2000);
            return;
        }
        _logger.LogInformation("Product removed: {Name} By UserID: {DeletedBy}", product.Name, 1);
        _productService.Delete(productId);
        WriteLine("Product removed successfully.");
        Thread.Sleep(2000);
    }
    public void UpdateProduct()
    {
        WriteLine("Update Product");
        WriteLine("--------------------");

        Write("Product ID: ");
        string? input = ReadLine();
        WriteLine();

        if (!int.TryParse(input, out int productId))
        {
            WriteLine("Invalid input. Please enter a valid product ID.");
            Thread.Sleep(2000);
            return;
        }

        Product? productToUpdate = _productService.GetById(productId);

        if (productToUpdate == null)
        {
            WriteLine("There is no product with that ID.");
            Thread.Sleep(2000);
            return;
        }

        Write("Name: ");
        string? name = ReadLine();

        Write("Description: ");
        string? description = ReadLine();

        Write("Price: ");
        string? priceInput = ReadLine();
        if (!decimal.TryParse(priceInput, out decimal price))
        {
            WriteLine("Invalid input. Please enter a valid price.");
            Thread.Sleep(2000);
            return;
        }

        Write("Quantity: ");
        string? quantityInput = ReadLine();
        WriteLine();

        if (!int.TryParse(quantityInput, out int quantity))
        {
            WriteLine("Invalid input. Please enter a valid quantity.");
            Thread.Sleep(2000);
            return;
        }

        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(description) && price > 0 && quantity > 0)
        {
            Product product = new()
            {
                Id = productId,
                Name = name,
                Description = description,
                Price = price,
                StockQuantity = quantity,
                CreatedBy = productToUpdate.CreatedBy,
                UpdatedBy = 1
            };
            _productService.Update(product);
            _logger.LogInformation("Product updated: {Name} By UserID: {UpdatedBy}", product.Name, product.UpdatedBy);

            WriteLine("Product updated successfully.");
            Thread.Sleep(2000);
            return;
        }

        WriteLine("Invalid input. Please enter valid data.");
        Thread.Sleep(2000);
        UpdateProduct();
    }
}


