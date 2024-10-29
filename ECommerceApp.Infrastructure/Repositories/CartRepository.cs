using ECommerceApp.Domain.Entities;
using ECommerceApp.Infrastructure.InMemoryDatabase;
using ECommerceApp.Infrastructure.Interfaces;

namespace ECommerceApp.Infrastructure.Repositories;

public class CartRepository : ICartRepository
{
    public ShoppingCart GetCart(int userId)
    {
        ShoppingCart shoppingCart = Database.ShoppingCarts.Find(c => c.UserId == userId) ??
            new ShoppingCart(GetNextId(), userId);
        Database.ShoppingCarts.Add(shoppingCart);
        return shoppingCart;
    }
    public void AddItemToCart(int userId, int productId)
    {
        // Retrieve the user's shopping cart
        ShoppingCart shoppingCart = Database.ShoppingCarts.Find(c => c.UserId == userId) ??
            throw new InvalidOperationException("Shopping cart not found for this user.");

        // Check if the product exists
        Product product = Database.Products.Find(p => p.ProductId == productId) ??
            throw new InvalidOperationException("Product not found.");

        // Check if the item is already in the cart
        CartItem? cartItem = shoppingCart.Items.Find(i => i.ProductId == productId);
        if (cartItem != null)
        {
            // Product already in cart, increase quantity
            if (cartItem.Quantity >= product.StockQuantity)
            {
                throw new InvalidOperationException("Product is out of stock.");
            }
            cartItem.Quantity++;
            cartItem.TotalPrice = cartItem.Quantity * product.Price;
        }
        else
        {
            // Product not in cart, add new CartItem
            shoppingCart.Items.Add(new CartItem
            {
                Product = product,
                ProductId = product.ProductId,
                Quantity = 1,
                TotalPrice = product.Price // Assuming total price per item here
            });
        }
    }
    public void RemoveItemFromCart(int userId, int productId)
    {
        // Retrieve the user's shopping cart
        ShoppingCart shoppingCart = Database.ShoppingCarts.Find(c => c.UserId == userId) ??
            throw new InvalidOperationException("Shopping cart not found for this user.");

        Product product = Database.Products.Find(p => p.ProductId == productId) ?? throw new InvalidOperationException("Product not found.");
        CartItem? cartItem = shoppingCart.Items.Find(item => item.ProductId == productId);

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
                shoppingCart.Items.Remove(cartItem);
            }
        }
    }
    public IEnumerable<CartItem> GetCartItems(int userId)
    {
        return Database.ShoppingCarts.Find(c => c.UserId == userId)?.Items ?? [];
    }
    public void ClearCart(int userId)
    {
        // Retrieve the user's shopping cart
        ShoppingCart shoppingCart = Database.ShoppingCarts.Find(c => c.UserId == userId) ??
            throw new InvalidOperationException("Shopping cart not found for this user.");

        // Clear the cart items
        shoppingCart.Items.Clear();
    }
    private int GetNextId()
    {
        return Database.ShoppingCarts.Count > 0 ? Database.ShoppingCarts.Max(c => c.CartId) + 1 : 1;
    }
}