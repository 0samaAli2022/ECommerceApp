
using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Application.Interfaces;

public interface IAuthService
{
    User? Login(string username, string password);
    void Logout();
    User Signup(User user);
    User? CurrentUser { get; }
}
