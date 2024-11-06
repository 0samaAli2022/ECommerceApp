namespace ECommerceApp.Domain.Entities;
public class CartItem : BaseEntity<int>
{
    public int ProductId { get; set; }
    public int CartId { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public Product Product { get; set; } = default!;
    public Cart Cart { get; set; } = default!;
}
