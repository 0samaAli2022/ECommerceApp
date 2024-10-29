using ECommerceApp.Models;
using ECommerceApp.Services;
using ECommerceApp.Views;

AuthService auth = AuthService.Instance;

ProductService productService = new();
productService.Add(
new Product
{
    ProductId = 1,
    Name = "Laptop",
    Description = "DELL laptop for gaming",
    Price = 1000.00m,
    StockQuantity = 10
}
);
productService.Add(
        new Product
        {
            ProductId = 2,
            Name = "Phone",
            Description = "Samsung phone",
            Price = 450.00m,
            StockQuantity = 4
        }
    );
productService.Add(
        new Product
        {
            ProductId = 3,
            Name = "Camera",
            Description = "Best camera in 2024",
            Price = 1500.00m,
            StockQuantity = 2
        }
    );
#endregion

var viewController = new AppViewController(auth);

viewController.Start();

