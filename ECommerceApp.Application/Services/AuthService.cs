using ECommerceApp.Application.Interfaces;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
namespace ECommerceApp.Application.Services;

public class AuthService(IUserRepository userRepository, ILogger<AuthService> logger) : IAuthService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ILogger<AuthService> _logger = logger;
    private User? currentUser = null; // To store current user's information
    public User? CurrentUser => currentUser;

    public User? Login(string username, string password)
    {
        currentUser = _userRepository.GetUserByCredentials(username);

        if (currentUser != null && currentUser.Password == password)
        {
            _logger.LogInformation("User {UserName} logged in.", currentUser.Username);
            return currentUser;
        }
        return null;
    }

    public void Logout()
    {
        _logger.LogInformation("User {UserName} logged out.", currentUser!.Username);
        currentUser = null;
    }

    public User Signup(User user)
    {
        _userRepository.AddUser(user);
        currentUser = user;
        _logger.LogInformation("User {UserName} signed up.", currentUser.Username);
        return user;
    }
}
