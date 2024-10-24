
namespace ECommerceApp.Interfaces;

public interface ICrud<T>
{
    T Add(T entity);
    T? Update(T entity);
    int Delete(int id);
}
