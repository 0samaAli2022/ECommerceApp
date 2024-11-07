using ECommerceApp.Application.Interfaces;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
namespace ECommerceApp.Application.Services;

public class AuthService(IUserRepository userRepository, ILogger<AuthService> logger) : IAuthService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ILogger<AuthService> _logger = logger;
    private bool isLoggedIn = false; // To track login state
    private User? currentUser = null; // To store current user's information

    public bool IsLoggedIn => isLoggedIn;

    public User? CurrentUser => currentUser;

    public User? Login(string username, string password)
    {
        currentUser = _userRepository.GetUserByCredentials(username, password);
        if (currentUser != null)
        {
            _logger.LogInformation("User {UserName} logged in.", currentUser.Username);
            isLoggedIn = true;
            return currentUser;
        }

        return null;
    }

    public void Logout()
    {
        _logger.LogInformation("User {UserName} logged out.", currentUser!.Username);
        currentUser = null;
        isLoggedIn = false;
    }

    public User Signup(User user)
    {
        _userRepository.AddUser(user);
        currentUser = user;
        isLoggedIn = true;
        _logger.LogInformation("User {UserName} signed up.", currentUser.Username);
        return user;
    }
}
