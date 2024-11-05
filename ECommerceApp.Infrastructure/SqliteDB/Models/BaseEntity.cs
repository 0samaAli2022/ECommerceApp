﻿namespace ECommerceApp.Infrastructure.SqliteDB.Models;

public abstract class BaseEntity<TId>
{
    public TId Id { get; set; } = default!;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; set; }
    public required int CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; } = false;
}