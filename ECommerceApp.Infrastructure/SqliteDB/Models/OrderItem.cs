﻿namespace ECommerceApp.Infrastructure.SqliteDB.Models;

public class OrderItem : BaseEntity<int>
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }

    public Order Order { get; set; } = default!;
    public Product Product { get; set; } = default!;
}
