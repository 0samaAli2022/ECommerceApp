using ECommerceApp.Application.Interfaces;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Infrastructure.Interfaces;

namespace ECommerceApp.Application.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    private readonly IProductRepository _productRepository = productRepository;

    public List<Product> GetAll()
    {
        return _productRepository.GetAll();
    }

    public Product GetById(int id)
    {
        return _productRepository.GetById(id);
    }

    public void Add(Product product)
    {
        _productRepository.Add(product);
    }
    public void Update(Product product)
    {
        _productRepository.Update(product);
    }
    public void Delete(int id)
    {
        _productRepository.Delete(id);
    }

}
