namespace ECommerceApp.Infrastructure.SqlServerDB.Models;

public class CartItem : BaseEntity<int>
{
    public int CartId { get; set; }
    public required Cart Cart { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}
