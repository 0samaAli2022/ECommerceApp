using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApp.Domain.Entities;
public class CartItem : BaseEntity<int>
{
    public int ProductId { get; set; }
    public int CartId { get; set; }
    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPrice { get; set; }
    public virtual Product? Product { get; set; }
    public virtual Cart? Cart { get; set; }
}
