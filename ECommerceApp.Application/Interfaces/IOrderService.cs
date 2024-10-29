using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Application.Interfaces;

public interface IOrderService
{
    void CreateOrder(ShoppingCart shoppingCart);
    List<Order> GetAll();
}
