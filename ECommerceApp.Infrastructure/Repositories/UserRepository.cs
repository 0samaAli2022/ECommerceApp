using ECommerceApp.Domain.Entities;
using ECommerceApp.Infrastructure.Interfaces;
using ECommerceApp.Infrastructure.SqlServerDB;

namespace ECommerceApp.Infrastructure.Repositories;

public class UserRepository(ECommerceDbContext context) : IUserRepository
{
    private readonly ECommerceDbContext _context = context;
    public User? GetUserByCredentials(string username, string password)
    {
        return _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
    }

    public void AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }
}
