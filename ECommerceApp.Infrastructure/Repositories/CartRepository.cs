using ECommerceApp.Domain.Entities;
using ECommerceApp.Infrastructure.Interfaces;
using ECommerceApp.Infrastructure.SqlServerDB;

namespace ECommerceApp.Infrastructure.Repositories;

public class CartRepository(ECommerceDbContext context) : ICartRepository
{
    private readonly ECommerceDbContext _context = context;
    public Cart GetCart(User user)
    {
        Cart? cart = user.ShoppingCart;
        if (cart != null)
        {
            return cart;
        }
        cart = new Cart()
        {
            UserId = user.Id,
            CreatedBy = user.Id,
        };
        _context.Carts.Add(cart);
        _context.SaveChanges();
        user.ShoppingCart = cart;
        return cart;
    }
    public void AddItemToCart(User user, Product product)
    {
        // Retrieve the user's shopping cart
        Cart shoppingCart = GetCart(user);

        // Check if the item is already in the cart
        CartItem? cartItem = FindCartItem(shoppingCart, product);
        if (cartItem != null)
        {
            // Product already in cart, increase quantity
            if (cartItem.Quantity >= product.StockQuantity)
            {
                throw new InvalidOperationException("Product is out of stock.");
            }
            cartItem.Quantity++;
            cartItem.TotalPrice = cartItem.Quantity * product.Price;
            shoppingCart.UpdatedAt = DateTime.Now;
            shoppingCart.UpdatedBy = user.Id;
        }
        else
        {
            // Product not in cart, add new CartItem
            shoppingCart.Items.Add(new CartItem
            {
                Product = product,
                ProductId = product.Id,
                Quantity = 1,
                TotalPrice = product.Price, // Assuming total price per item here
                CreatedBy = user.Id,
            });
        }
        shoppingCart.TotalPrice = shoppingCart.Items.Sum(i => i.TotalPrice);
        user.ShoppingCart = shoppingCart;
        _context.SaveChanges();
    }
    public void RemoveItemFromCart(User user, Product product)
    {
        // Retrieve the user's shopping cart
        Cart cart = GetCart(user);

        CartItem cartItem = FindCartItem(cart, product) ??
            throw new InvalidOperationException("Product not found in cart.");

        DecreaseCartItemQuantity(cartItem, cart, product);

        cart.UpdatedAt = DateTime.Now;
        cart.UpdatedBy = user.Id;
        cart.TotalPrice = cart.Items.Sum(i => i.TotalPrice);
        user.ShoppingCart = cart;
        _context.SaveChanges();
    }
    public void ClearCart(User user)
    {
        // Retrieve the user's shopping cart
        Cart shoppingCart = GetCart(user);

        // Clear the cart items
        shoppingCart.Items.Clear();

        shoppingCart.UpdatedAt = DateTime.Now;
        shoppingCart.UpdatedBy = user.Id;
        shoppingCart.TotalPrice = 0;
        user.ShoppingCart = shoppingCart;
        _context.SaveChanges();
    }

    private CartItem? FindCartItem(Cart cart, Product product)
    {
        return cart.Items.FirstOrDefault(item => item.ProductId == product.Id) ?? null;
    }
    private void DecreaseCartItemQuantity(CartItem cartItem, Cart cart, Product product)
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
            cart.Items.Remove(cartItem);
        }
    }
}