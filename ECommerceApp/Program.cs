
using ECommerceApp.Application.Interfaces;
using ECommerceApp.Application.Services;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Infrastructure.Interfaces;
using ECommerceApp.Infrastructure.Repositories;
using ECommerceApp.Views;
using Microsoft.Extensions.DependencyInjection;


var serviceProvider = new ServiceCollection()
    .AddSingleton<IUserRepository, UserRepository>()
    .AddSingleton<IAuthService, AuthService>()
    .AddSingleton<IProductRepository, ProductRepository>()
    .AddSingleton<IProductService, ProductService>()
    .AddSingleton<ICartRepository, CartRepository>()
    .AddSingleton<ICartService, CartService>()
    .AddSingleton<IOrderRepository, OrderRepository>()
    .AddSingleton<IOrderService, OrderService>()
    .AddSingleton<AppViewController>()
    .AddSingleton<CartView>()
    .AddSingleton<ProductView>()
    .AddSingleton<OrderView>()
    .AddSingleton<AdminView>()
    .BuildServiceProvider();

#region Add Products
var productService = serviceProvider.GetRequiredService<IProductService>();
productService.Add(
new Product
{
    Name = "Laptop",
    Description = "DELL laptop for gaming",
    Price = 1000.00m,
    StockQuantity = 10
}
);
productService.Add(
        new Product
        {
            Name = "Phone",
            Description = "Samsung phone",
            Price = 450.00m,
            StockQuantity = 4
        }
    );
productService.Add(
        new Product
        {
            Name = "Camera",
            Description = "Best camera in 2024",
            Price = 1500.00m,
            StockQuantity = 2
        }
    );
#endregion

try
{
    AppViewController viewController = serviceProvider.GetRequiredService<AppViewController>();
    viewController.Start();
}
catch (Exception e)
{

    Console.WriteLine($"An error occurred: {e.Message}");
    Console.WriteLine($"Stack Trace: {e.StackTrace}");
}

