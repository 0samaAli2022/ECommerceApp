using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApp.Domain.Entities;

public class Order : BaseEntity<int>
{
    public required int UserId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    [Column(TypeName = "decimal(18,2)")]
    public required decimal TotalAmount { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; } = [];
    public virtual User? User { get; set; }
}
