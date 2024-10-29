namespace ECommerceApp.Domain.Entities;

public class ShoppingCart(int cartId, int userId)  // primary constructor
{
    public int CartId { get; set; } = cartId;
    public int UserId { get; set; } = userId;
    public List<CartItem> Items { get; set; } = [];
    public decimal TotalPrice => Items.Sum(item => item.TotalPrice);
}
