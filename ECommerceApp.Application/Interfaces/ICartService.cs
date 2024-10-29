using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Application.Interfaces;

public interface ICartService
{
    ShoppingCart GetCart();
    void AddToCart(int productId);
    void RemoveFromCart(int productId);
    IEnumerable<CartItem> GetCartItems();
    void ClearCart();
}
