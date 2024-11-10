namespace ECommerceApp.Domain.Entities;

public class Cart : BaseEntity<int>  // primary constructor
{
    public int UserId { get; set; }
    public decimal TotalPrice { get; set; } = 0m;
    public virtual ICollection<CartItem> Items { get; set; } = [];
    public virtual User? User { get; set; }
}
