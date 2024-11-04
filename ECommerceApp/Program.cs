
using ECommerceApp.Application.Interfaces;
using ECommerceApp.Application.Services;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Infrastructure.Interfaces;
using ECommerceApp.Infrastructure.Repositories;
using ECommerceApp.Infrastructure.SqliteDB;
using ECommerceApp.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

try
{
    IHost host = AppStartup();
    #region Add Products
    var productService = host.Services.GetRequiredService<IProductService>();
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
    AppViewController viewController = host.Services.GetRequiredService<AppViewController>();
    viewController.Start();
}
catch (Exception e)
{

    WriteLine($"An error occurred: {e.Message}");
    WriteLine($"Stack Trace: {e.StackTrace}");
}

static void ConfigSetup(IConfigurationBuilder builder)
{
    builder.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();
}

static IHost AppStartup()
{
    var builder = new ConfigurationBuilder();
    ConfigSetup(builder);

    // definig Serilog Configs
    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Build())
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .CreateLogger();

    // Initiated the dependency injection container
    var host = Host.CreateDefaultBuilder()
        .UseSerilog()
        .ConfigureServices((context, services) =>
        {
            services.AddDbContext<ECommerceDbContext>(
                options => options.UseSqlite(context.Configuration.GetConnectionString("DefaultConnection"))
                );
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<AppViewController>();
            services.AddScoped<CartView>();
            services.AddScoped<ProductView>();
            services.AddScoped<OrderView>();
            services.AddScoped<AdminView>();
        })
        .Build();
    return host;
}