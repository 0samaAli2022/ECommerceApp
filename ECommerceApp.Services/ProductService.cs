using ECommerceApp.Data;
using ECommerceApp.Interfaces;
using ECommerceApp.Models;

namespace ECommerceApp.Services;

public class ProductService : ICrud<Product>
{
    public Product Add(Product product)
    {
        Database.Products.Add(product);
        return product;
    }

    public int Delete(int id)
    {
        return Database.Products.RemoveAll(p => p.ProductId == id);
    }

    public List<Product> GetAll()
    {
        return Database.Products;
    }

    public Product? GetById(int id)
    {
        return Database.Products.Find(p => p.ProductId == id);
    }

    public Product? Update(Product product)
    {
        Product? updateProduct = Database.Products.Find(p => p.ProductId == product.ProductId);
        if (updateProduct != null)
        {
            updateProduct.ProductId = product.ProductId;
            updateProduct.Price = product.Price;
            updateProduct.Description = product.Description;
            updateProduct.Name = product.Name;
            updateProduct.StockQuantity = product.StockQuantity;
            return updateProduct;
        }
        return null;
    }

    public int GetNextId()
    {
        return Database.Products.Count + 1;
    }

}
