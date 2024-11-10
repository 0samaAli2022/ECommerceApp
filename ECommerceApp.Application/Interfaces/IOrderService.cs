using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Application.Interfaces;

public interface IOrderService
{
    void CreateOrder();
    List<Order> GetAll();
}
