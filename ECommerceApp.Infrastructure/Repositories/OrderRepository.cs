using ECommerceApp.Domain.Entities;
using ECommerceApp.Infrastructure.InMemoryDatabase;
using ECommerceApp.Infrastructure.Interfaces;

namespace ECommerceApp.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    public void Add(Order order)
    {
        order.OrderId = GetNextId();
        Database.Orders.Add(order);
    }

    public List<Order> GetAllByUserId(int userId)
    {
        return Database.Orders.Where(o => o.UserId == userId).ToList();
    }

    private int GetNextId()
    {
        return Database.Orders.Count > 0 ? Database.Orders.Max(o => o.OrderId) + 1 : 1;
    }
}
