using ECommerceApp.Data;
using ECommerceApp.Models;

public static class AuthView
{
    public static User? SignUp()
    {
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
        if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(email))
        {
            return new User
            {
                UserId = Database.Users.Count + 1,
                Username = username,
                Password = password,
                Email = email,
                Address = address
            };
        }

        return null;
    }

    public static (string? Username, string? Password) SignIn()
    {
        WriteLine("Sign In");
        WriteLine("--------------");

        Write("Username: ");
        string? username = ReadLine();

        Write("Password: ");
        string? password = ReadLine();
        WriteLine();
        return (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            ? (username, password)
            : (null, null);
    }
}
