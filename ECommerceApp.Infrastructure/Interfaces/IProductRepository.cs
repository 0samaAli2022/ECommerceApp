
using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Infrastructure.Interfaces;

public interface IProductRepository
{
    Product GetById(int id);
    List<Product> GetAll();
    void Add(Product product);
    void Update(Product product);
    void Delete(int id);
}
