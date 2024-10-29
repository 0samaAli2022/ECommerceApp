
using ECommerceApp.Data;
using ECommerceApp.Models;

namespace ECommerceApp.Services;

public class CartService
{
    public ShoppingCart ShoppingCart { get; set; }
    private readonly AuthService _authService;

    public CartService(AuthService authService)
    {
        _authService = authService;

        if (_authService.IsLoggedIn && _authService.CurrentUser != null)
        {
            // Initialize the cart with the authenticated user's ID
            ShoppingCart = Database.ShoppingCarts.FirstOrDefault(c => c.UserId == _authService.CurrentUser.UserId)
                           ?? new ShoppingCart(Database.ShoppingCarts.Count + 1, _authService.CurrentUser.UserId);
            _authService.CurrentUser.ShoppingCart = ShoppingCart;
        }
        else
        {
            throw new InvalidOperationException("User must be logged in to access the cart.");
        }
    }

    // Adds a product to the cart, or increases quantity if already present
    public void AddToCart(int productId)
    {
        var cartItem = ShoppingCart.Items.FirstOrDefault(item => item.ProductId == productId);
        var product = Database.Products.FirstOrDefault(p => p.ProductId == productId);

        if (product == null)
        {
            throw new InvalidOperationException("Product not found.");
        }

        if (cartItem != null)
        {
            // Product already in cart, increase quantity
            if (cartItem.Quantity == product.StockQuantity)
            {
                throw new InvalidOperationException("Product is out of stock.");
            }
            cartItem.Quantity++;
            cartItem.TotalPrice = cartItem.Quantity * product.Price;
        }
        else
        {
            // Product not in cart, add new CartItem
            ShoppingCart.Items.Add(new CartItem
            {
                Product = product,
                ProductId = product.ProductId,
                Quantity = 1,
                TotalPrice = product.Price // Assuming total price per item here
            });
        }

        SaveCart();
    }

    // Decreases product quantity or removes it if the quantity reaches zero
    public void RemoveFromCart(int productId)
    {
        var cartItem = ShoppingCart.Items.FirstOrDefault(item => item.ProductId == productId);
        var product = Database.Products.FirstOrDefault(p => p.ProductId == productId) ?? throw new InvalidOperationException("Product not found.");

        if (cartItem != null)
        {
            if (cartItem.Quantity > 1)
            {
                // Decrease quantity if more than 1
                cartItem.Quantity--;
                cartItem.TotalPrice = cartItem.Quantity * product.Price;
            }
            else
            {
                // Remove item if quantity is 1 or less
                ShoppingCart.Items.Remove(cartItem);
            }
        }

        SaveCart();
    }

    // Clear all items from the cart
    public void ClearCart() => ShoppingCart.Items.Clear();

    // Save the cart, ensuring only one cart per user
    public void SaveCart()
    {
        var existingCart = Database.ShoppingCarts.FirstOrDefault(c => c.UserId == ShoppingCart.UserId);
        if (existingCart == null)
        {
            // If no cart exists for this user, add it
            Database.ShoppingCarts.Add(ShoppingCart);
        }
        else
        {
            // If an existing cart exists, update it
            existingCart.Items = ShoppingCart.Items;
        }
        _authService.CurrentUser!.ShoppingCart = ShoppingCart;
    }
}

