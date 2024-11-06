using ECommerceApp.Application.Interfaces;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Infrastructure.Interfaces;

namespace ECommerceApp.Application.Services
{
    public class OrderService(IOrderRepository orderRepository, IAuthService authService, IProductService productService) : IOrderService
    {
        private readonly IAuthService _authService = authService;
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IProductService _productService = productService;


        public void CreateOrder(Cart shoppingCart)
        {
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

            _orderRepository.Add(order); // Use repository to add order

            foreach (var item in shoppingCart.Items)
            {
                item.Product.StockQuantity -= item.Quantity;
                if (item.Product.StockQuantity <= 0)
                {
                    _productService.Delete(item.ProductId);
                }
            }
            // Clear shopping cart
            shoppingCart.Items.Clear();
        }

        public List<Order> GetAll()
        {
            return _orderRepository.GetAllByUserId(_authService.CurrentUser!.Id);
        }
    }
}
