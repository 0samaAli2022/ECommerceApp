using ECommerceApp.Application.Interfaces;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Infrastructure.Interfaces;

namespace ECommerceApp.Application.Services;

public class CartService(IAuthService authService, ICartRepository cartRepository) : ICartService
{
    private readonly IAuthService _authService = authService;
    private readonly ICartRepository _cartRepository = cartRepository;

    public ShoppingCart GetCart() => _cartRepository.GetCart(_authService.CurrentUser!.UserId);

    // Adds a product to the cart, or increases quantity if already present
    public void AddToCart(int productId)
    {
        _cartRepository.AddItemToCart(_authService.CurrentUser!.UserId, productId);
    }

    // Decreases product quantity or removes it if the quantity reaches zero
    public void RemoveFromCart(int productId)
    {
        _cartRepository.RemoveItemFromCart(_authService.CurrentUser!.UserId, productId);
        //SaveCart();
    }

    // Clear all items from the cart
    public void ClearCart() => _cartRepository.ClearCart(_authService.CurrentUser!.UserId);

    // Save the cart, ensuring only one cart per user
    //public void SaveCart()
    //{
    //    var existingCart = Database.ShoppingCarts.FirstOrDefault(c => c.UserId == ShoppingCart.UserId);
    //    if (existingCart == null)
    //    {
    //        // If no cart exists for this user, add it
    //        Database.ShoppingCarts.Add(ShoppingCart);
    //    }
    //    else
    //    {
    //        // If an existing cart exists, update it
    //        existingCart.Items = ShoppingCart.Items;
    //    }
    //    _authService.CurrentUser!.ShoppingCart = ShoppingCart;
    //}

    public IEnumerable<CartItem> GetCartItems()
    {
        return _cartRepository.GetCartItems(_authService.CurrentUser!.UserId);
    }
}

