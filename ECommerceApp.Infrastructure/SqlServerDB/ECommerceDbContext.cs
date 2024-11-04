using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Infrastructure.SqlServerDB;

public class ECommerceDbContext : DbContext
{
    public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options) { }

}
