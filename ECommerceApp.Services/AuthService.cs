

using ECommerceApp.Data;
using ECommerceApp.Models;

namespace ECommerceApp.Services;

// a singleton to provide authentication services to the application
public sealed class AuthService
{
    private static AuthService? _instance = null;
    private AuthService() { }

    private bool isLoggedIn = false; // To track login state
    private User? currentUser = null; // To store current user's information
    public static AuthService Instance => _instance ??= new AuthService();
    public bool IsLoggedIn => Instance.isLoggedIn;
    public User? CurrentUser => Instance.currentUser;


    public User? Login(string username, string password)
    {
        User? user = Database.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        if (user != null)
        {
            Instance.currentUser = user;
            Instance.isLoggedIn = true;
            return user;
        }

        return null;
    }

    public void Logout()
    {
        Instance.currentUser = null;
        Instance.isLoggedIn = false;
    }

    public User Signup(User user)
    {
        Database.Users.Add(user);
        Instance.currentUser = user;
        Instance.isLoggedIn = true;
        return user;
    }
}
