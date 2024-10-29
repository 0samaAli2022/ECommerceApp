using ECommerceApp.Application.Interfaces;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Infrastructure.Interfaces;

namespace ECommerceApp.Application.Services;

public class AuthService(IUserRepository userRepository) : IAuthService
{
    private readonly IUserRepository _userRepository = userRepository;
    private bool isLoggedIn = false; // To track login state
    private User? currentUser = null; // To store current user's information

    public bool IsLoggedIn => isLoggedIn;

    public User? CurrentUser => currentUser;

    public User? Login(string username, string password)
    {
        currentUser = _userRepository.GetUserByCredentials(username, password);
        if (currentUser != null)
        {
            isLoggedIn = true;
            return currentUser;
        }

        return null;
    }

    public void Logout()
    {
        currentUser = null;
        isLoggedIn = false;
    }

    public User Signup(User user)
    {
        _userRepository.AddUser(user);
        currentUser = user;
        isLoggedIn = true;
        return user;
    }
}
