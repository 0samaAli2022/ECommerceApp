using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Infrastructure.Interfaces;

public interface IUserRepository
{
    User? GetUserByCredentials(string username);
    void AddUser(User user);
}
