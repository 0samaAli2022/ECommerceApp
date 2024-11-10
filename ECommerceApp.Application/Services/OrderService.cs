using ECommerceApp.Application.Interfaces;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace ECommerceApp.Application.Services
{
    public class OrderService(IOrderRepository orderRepository, IAuthService authService,
        IProductService productService, ICartService cartService, ILogger<OrderService> logger) : IOrderService
    {
        private readonly IAuthService _authService = authService;
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IProductService _productService = productService;
        private readonly ICartService _cartService = cartService;
        private readonly ILogger<OrderService> _logger = logger;


        public void CreateOrder()
        {
            var shoppingCart = _cartService.GetCart();
            var orderItems = shoppingCart.Items.Select(item => new OrderItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                TotalPrice = item.TotalPrice,
                Product = item.Product,
                CreatedBy = _authService.CurrentUser!.Id
            }).ToList();
            // Save order to database
            Order order = new()
            {
                UserId = shoppingCart.UserId,
                TotalAmount = shoppingCart.TotalPrice,
                OrderItems = orderItems, // Create a new list of OrderItem objects
                OrderDate = DateTime.Now,
                CreatedBy = _authService.CurrentUser!.Id
            };
            UpdateQuantity(shoppingCart);
            // Clear shopping cart
            _cartService.ClearCart();
            _orderRepository.Add(order); // Use repository to add order to database and save changes
            _logger.LogInformation("New order: {OrderId} placed by userId: {UserId} successfuly.", order.Id, order.CreatedBy);
        }

        public List<Order> GetAll()
        {
            return _orderRepository.GetAll(_authService.CurrentUser!);
        }

        private void UpdateQuantity(Cart cart)
        {
            foreach (var item in cart.Items)
            {
                item.Product!.StockQuantity -= item.Quantity;
                if (item.Product.StockQuantity <= 0)
                {
                    _productService.Delete(item.ProductId);
                    _logger.LogInformation("Product {ProductId} is out of stock and has been deleted.", item.ProductId);
                }
            }
        }
    }
}
