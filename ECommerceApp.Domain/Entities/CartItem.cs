
namespace ECommerceApp.Domain.Entities;

public class CartItem
{
    public int ProductId { get; set; }
    public required Product Product { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}
