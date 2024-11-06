﻿
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApp.Domain.Entities;
public class User : BaseEntity<int>
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string? Address { get; set; }

    [InverseProperty("User")]
    public Cart ShoppingCart { get; set; } = default!;
    public ICollection<Order> Orders { get; set; } = [];
}
