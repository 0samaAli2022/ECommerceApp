namespace ECommerceApp.Infrastructure.SqlServerDB.Models;

public abstract class BaseEntity<TId>
{
    public required TId Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
}
