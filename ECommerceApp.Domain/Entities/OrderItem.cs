using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApp.Domain.Entities;

public class OrderItem : BaseEntity<int>
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPrice { get; set; }

    public virtual Order Order { get; set; } = default!;
    public virtual Product Product { get; set; } = default!;
}
