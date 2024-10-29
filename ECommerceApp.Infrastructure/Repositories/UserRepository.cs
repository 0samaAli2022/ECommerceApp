using ECommerceApp.Domain.Entities;
using ECommerceApp.Infrastructure.InMemoryDatabase;
using ECommerceApp.Infrastructure.Interfaces;

namespace ECommerceApp.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    public User? GetUserByCredentials(string username, string password)
    {
        return Database.Users.Find(u => u.Username == username && u.Password == password);
    }

    public void AddUser(User user)
    {
        user.UserId = GetNextId();
        Database.Users.Add(user);
    }

    private int GetNextId()
    {
        return Database.Users.Count > 0 ? Database.Users.Max(u => u.UserId) + 1 : 1;
    }
}
