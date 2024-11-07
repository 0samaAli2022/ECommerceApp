using ECommerceApp.Application.Interfaces;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Infrastructure.Interfaces;

namespace ECommerceApp.Application.Services;

public class CartService(IAuthService authService, ICartRepository cartRepository) : ICartService
{
    private readonly IAuthService _authService = authService;
    private readonly ICartRepository _cartRepository = cartRepository;

    public Cart GetCart() => _cartRepository.GetCart(_authService.CurrentUser!.Id);

    // Adds a product to the cart, or increases quantity if already present
    public void AddToCart(int productId)
    {
        _cartRepository.AddItemToCart(_authService.CurrentUser!.Id, productId);
    }

    // Decreases product quantity or removes it if the quantity reaches zero
    public void RemoveFromCart(int productId)
    {
        _cartRepository.RemoveItemFromCart(_authService.CurrentUser!.Id, productId);
    }

    // Clear all items from the cart
    public void ClearCart() => _cartRepository.ClearCart(_authService.CurrentUser!.Id);

}

