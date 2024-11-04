namespace ECommerceApp.Infrastructure.SqlServerDB.Models;

public class User : BaseEntity<int>
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string? Address { get; set; } = null;
}
