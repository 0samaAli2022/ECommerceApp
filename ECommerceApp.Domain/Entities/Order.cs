
namespace ECommerceApp.Domain.Entities;

public class Order : BaseEntity<int>
{
    public required int UserId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public required decimal TotalAmount { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = [];
    public User User { get; set; } = default!;
}
