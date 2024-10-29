using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Infrastructure.Interfaces;

public interface ICartRepository
{
    ShoppingCart GetCart(int userId);
    void AddItemToCart(int userId, int productId);
    void RemoveItemFromCart(int userId, int productId);
    IEnumerable<CartItem> GetCartItems(int userId);
    void ClearCart(int userId);
}
