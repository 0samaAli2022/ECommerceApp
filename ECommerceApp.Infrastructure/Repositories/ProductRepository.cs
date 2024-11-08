using ECommerceApp.Domain.Entities;
using ECommerceApp.Infrastructure.Interfaces;
using ECommerceApp.Infrastructure.SqlServerDB;

namespace ECommerceApp.Infrastructure.Repositories;

public class ProductRepository(ECommerceDbContext context) : IProductRepository
{
    private readonly ECommerceDbContext _context = context;
    public void Add(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        Product? product = GetById(id);
        _context.Products.Remove(product);
        _context.SaveChanges();
    }

    public List<Product> GetAll()
    {
        return [.. _context.Products];
    }

    public Product GetById(int id)
    {
        Product product = _context.Products.FirstOrDefault(p => p.Id == id) ??
            throw new KeyNotFoundException($"Product with ID {id} not found.");
        return product;
    }

    public void Update(Product product)
    {
        Product? updateProduct = GetById(product.Id);
        updateProduct.Price = product.Price;
        updateProduct.Description = product.Description;
        updateProduct.Name = product.Name;
        updateProduct.StockQuantity = product.StockQuantity;
        _context.SaveChanges();
    }
}
