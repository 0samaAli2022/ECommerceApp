using ECommerceApp.Application.Interfaces;
using ECommerceApp.Application.Services;
using ECommerceApp.Infrastructure.Interfaces;
using ECommerceApp.Infrastructure.Repositories;
using ECommerceApp.Infrastructure.SqlServerDB;
using ECommerceApp.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

try
{
    IHost host = AppStartup();
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

    // defining Serilog Configs
    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Build())
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .CreateLogger();

    // Initiating the dependency injection container
    var host = Host.CreateDefaultBuilder()
        .UseSerilog()
        .ConfigureServices((context, services) =>
        {
            services.AddDbContext<ECommerceDbContext>(
                options => options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection"))
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