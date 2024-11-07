using ECommerceApp.Domain.Entities;
using ECommerceApp.Infrastructure.Interfaces;
using ECommerceApp.Infrastructure.SqlServerDB;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Infrastructure.Repositories;

public class CartRepository(ECommerceDbContext context) : ICartRepository
{
    private readonly ECommerceDbContext _context = context;
    public Cart GetCart(int userId)
    {
        Cart? cart = _context.Carts.Include(c => c.Items).ThenInclude(i => i.Product).FirstOrDefault(c => c.UserId == userId);
        if (cart != null)
        {
            return cart;
        }
        cart = new Cart()
        {
            UserId = userId,
            CreatedBy = userId,
        };
        _context.Carts.Add(cart);
        _context.SaveChanges();
        return cart;
    }
    public void AddItemToCart(int userId, int productId)
    {
        // Retrieve the user's shopping cart
        Cart shoppingCart = _context.Carts.FirstOrDefault(c => c.UserId == userId) ??
            throw new InvalidOperationException("Shopping cart not found for this user.");

        // Check if the product exists
        Product product = _context.Products.FirstOrDefault(p => p.Id == productId) ??
            throw new InvalidOperationException("Product not found.");

        // Check if the item is already in the cart
        CartItem? cartItem = shoppingCart.Items.FirstOrDefault(i => i.ProductId == productId);
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
                ProductId = product.Id,
                Quantity = 1,
                TotalPrice = product.Price, // Assuming total price per item here
                CreatedBy = userId,
            });
        }
        _context.SaveChanges();
    }
    public void RemoveItemFromCart(int userId, int productId)
    {
        // Retrieve the user's shopping cart
        Cart shoppingCart = _context.Carts.FirstOrDefault(c => c.UserId == userId) ??
            throw new InvalidOperationException("Shopping cart not found for this user.");

        Product product = _context.Products.FirstOrDefault(p => p.Id == productId) ?? throw new InvalidOperationException("Product not found.");
        CartItem cartItem = shoppingCart.Items.FirstOrDefault(item => item.ProductId == productId) ??
            throw new InvalidOperationException("Product not found in cart.");


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


        _context.SaveChanges();
    }
    public void ClearCart(int userId)
    {
        // Retrieve the user's shopping cart
        Cart shoppingCart = _context.Carts.FirstOrDefault(c => c.UserId == userId) ??
            throw new InvalidOperationException("Shopping cart not found for this user.");

        // Clear the cart items
        shoppingCart.Items.Clear();

        _context.SaveChanges();
    }
}