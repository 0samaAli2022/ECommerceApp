﻿
namespace ECommerceApp.Domain.Entities;

public class Order
{
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public List<CartItem> OrderItems { get; set; } = [];
    public decimal TotalAmount { get; set; }
}