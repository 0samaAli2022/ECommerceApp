
namespace ECommerceApp.Domain.Entities;

public class Product : BaseEntity<int>
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required decimal Price { get; set; }
    public required int StockQuantity { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; } = [];
    public ICollection<CartItem> CartItems { get; set; } = [];
}
