using ECommerceApp.Domain.Entities;
using ECommerceApp.Infrastructure.Interfaces;
using ECommerceApp.Infrastructure.SqlServerDB;

namespace ECommerceApp.Infrastructure.Repositories;

public class OrderRepository(ECommerceDbContext context) : IOrderRepository
{
    private readonly ECommerceDbContext _context = context;
    public void Add(Order order)
    {
        _context.Orders.Add(order);
        _context.SaveChanges();
    }

    public List<Order> GetAll(User user)
    {
        return user.Orders!.ToList();
    }
}
