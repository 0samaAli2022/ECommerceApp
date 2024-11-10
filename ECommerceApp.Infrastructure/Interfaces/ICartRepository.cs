using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Infrastructure.Interfaces;

public interface ICartRepository
{
    Cart GetCart(User user);
    void AddItemToCart(User user, Product product);
    void RemoveItemFromCart(User user, Product product);
    void ClearCart(User user);
}
