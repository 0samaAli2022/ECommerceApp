using ECommerceApp.Domain.Entities;
using ECommerceApp.Infrastructure.InMemoryDatabase;
using ECommerceApp.Infrastructure.Interfaces;

namespace ECommerceApp.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    public void Add(Product product)
    {
        product.ProductId = GetNextId();
        Database.Products.Add(product);
    }

    public void Delete(int id)
    {
        Database.Products.RemoveAll(p => p.ProductId == id);
    }

    public List<Product> GetAll()
    {
        return Database.Products;
    }

    public Product GetById(int id)
    {
        Product product = Database.Products.Find(p => p.ProductId == id) ??
            throw new KeyNotFoundException($"Product with ID {id} not found.");
        return product;
    }

    public void Update(Product product)
    {
        Product? updateProduct = Database.Products.Find(p => p.ProductId == product.ProductId) ??
            throw new KeyNotFoundException($"Product with ID {product.ProductId} not found.");
        updateProduct.Price = product.Price;
        updateProduct.Description = product.Description;
        updateProduct.Name = product.Name;
        updateProduct.StockQuantity = product.StockQuantity;
    }

    private int GetNextId()
    {
        return Database.Products.Count > 0 ? Database.Products.Max(p => p.ProductId) + 1 : 1;
    }
}
