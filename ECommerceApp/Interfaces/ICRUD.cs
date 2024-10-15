
namespace ECommerceApp.Interfaces;

public interface ICRUD<T>
{
    T Add(T entity);
    T? Update(T entity);
    void Delete(int id);
    //T? GetById(int id);
    //List<T> GetAll();
}
