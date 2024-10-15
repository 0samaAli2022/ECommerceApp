using ECommerceApp.Data;
using ECommerceApp.Interfaces;
using ECommerceApp.Models;

namespace ECommerceApp.Services;

public class ProductService : ICRUD<Product>
{
    public Product Add(Product product)
    {
        Database.Products.Add(product);
        return product;
    }

    public void Delete(int id)
    {
        Database.Products.RemoveAll(p => p.ProductId == id);
    }

    public List<Product> GetAll()
    {
        return Database.Products;
    }

    public Product? GetById(int id)
    {
        return Database.Products.FirstOrDefault(p => p.ProductId == id);
    }

    public Product? Update(Product product)
    {
        Product? updateProduct = Database.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
        if (updateProduct != null)
        {
            updateProduct.ProductId = product.ProductId;
            updateProduct.Price = product.Price;
            updateProduct.Description = product.Description;
            updateProduct.Name = product.Name;
            return updateProduct;
        }
        return null;
    }

    public int GetNextId()
    {
        return Database.Products.Count + 1;
    }
}
