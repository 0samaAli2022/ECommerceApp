using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Views;

public static class AuthView
{
    public static User? SignUp()
    {
        Clear();
        WriteLine("Sign Up");
        WriteLine("--------------");

        Write("Username: ");
        string? username = ReadLine();

        Write("Password: ");
        string? password = ReadLine();

        Write("Email: ");
        string? email = ReadLine();

        Write("Address: ");
        string? address = ReadLine();
        WriteLine();
        WriteLine("Signing in...");
        WriteLine();
        Thread.Sleep(2000);

        if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(email))
        {
            return new User
            {
                Username = username,
                Password = password,
                Email = email,
                Address = address,
                CreatedBy = 1
            };
        }

        return null;
    }

    public static (string? Username, string? Password) SignIn()
    {
        Clear();
        WriteLine("Sign In");
        WriteLine("--------------");

        Write("Username: ");
        string? username = ReadLine();

        Write("Password: ");
        string? password = ReadLine();
        WriteLine();
        WriteLine("Signing in...");
        WriteLine();
        Thread.Sleep(2000);
        return (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            ? (username, password)
            : (null, null);
    }
}
