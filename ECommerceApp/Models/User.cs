
namespace ECommerceApp.Models;
public class User
{
    public int UserId { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string? Address { get; set; }

    public ShoppingCart? ShoppingCart { get; set; }
}
