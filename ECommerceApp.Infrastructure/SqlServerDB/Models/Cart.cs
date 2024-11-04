namespace ECommerceApp.Infrastructure.SqlServerDB.Models;

public class Cart : BaseEntity<int>
{
    public int UserId { get; set; }
    public decimal TotalPrice { get; set; }
}
