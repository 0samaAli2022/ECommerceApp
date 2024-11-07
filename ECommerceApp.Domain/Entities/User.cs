namespace ECommerceApp.Domain.Entities;
public class User : BaseEntity<int>
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string? Address { get; set; }
    public virtual Cart ShoppingCart { get; set; } = default!;
    public virtual ICollection<Order> Orders { get; set; } = [];
}
