
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApp.Domain.Entities;

public class Product : BaseEntity<int>
{
    public required string Name { get; set; }
    public string? Description { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public required decimal Price { get; set; }
    public required int StockQuantity { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = null!;
    public virtual ICollection<CartItem> CartItems { get; set; } = null!;
}
