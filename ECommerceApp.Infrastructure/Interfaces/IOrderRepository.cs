using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Infrastructure.Interfaces;

public interface IOrderRepository
{
    void Add(Order order);
    List<Order> GetAll(User user);
}
