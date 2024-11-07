using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Infrastructure.Interfaces;

public interface ICartRepository
{
    Cart GetCart(int userId);
    void AddItemToCart(int userId, int productId);
    void RemoveItemFromCart(int userId, int productId);
    void ClearCart(int userId);
}
