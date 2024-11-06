using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApp.Domain.Entities;

public class Cart : BaseEntity<int>  // primary constructor
{
    public int UserId { get; set; }
    public decimal TotalPrice => Items.Sum(item => item.TotalPrice);
    public ICollection<CartItem> Items { get; set; } = [];

    [ForeignKey("UserId")]
    public User User { get; set; } = default!;
}
