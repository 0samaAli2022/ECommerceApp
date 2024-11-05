namespace ECommerceApp.Infrastructure.SqlServerDB.Models;

public class Cart : BaseEntity<int>  // primary constructor
{
    public int UserId { get; set; }
    public decimal TotalPrice => Items.Sum(item => item.TotalPrice);
    public ICollection<CartItem> Items { get; set; } = [];
    public User User { get; set; } = default!;
}
