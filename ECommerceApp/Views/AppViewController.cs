
using ECommerceApp.Models;
using ECommerceApp.Services;

namespace ECommerceApp.Views;

public class AppViewController(AuthService authService)
{
    private readonly AuthService _authService = authService;
    private CartService? _cartService;
    private OrderService? _orderService;

    public void Start()
    {
        while (true)
        {
            Clear();
            WriteLine("E-Commerce App");
            WriteLine("--------------");
            WriteLine("1. Login");
            WriteLine("2. Sign Up");
            WriteLine("3. Exit");
            WriteLine("--------------");
            Write("Enter your choice: ");
            string? choice = ReadLine();
            WriteLine();
            switch (choice)
            {
                case "1":
                    SignInFlow();
                    break;
                case "2":
                    SignUpFlow();
                    break;
                case "3":
                    return;
                default:
                    WriteLine("Invalid choice.");
                    WriteLine("--------------");

                    break;
            }
        }
    }

    private void SignInFlow()
    {
        var (username, password) = AuthView.SignIn();

        if (username != null && password != null)
        {
            var user = _authService.Login(username, password);

            if (user != null)
            {
                WriteLine($"Welcome, {user.Username}!");
                WriteLine("--------------");
                WriteLine();
                Thread.Sleep(2000);
                // Initialize the cart with the authenticated user's ID
                _cartService = new CartService(_authService);

                // Initialize the order service with the authenticated user's ID
                _orderService = new OrderService(_authService);

                AfterLoginFlow();
            }
            else
            {
                WriteLine("Invalid credentials. Try again.");
                WriteLine("-------------------------------");
                WriteLine();
                Thread.Sleep(2000);
            }
        }
    }

    private void SignUpFlow()
    {
        User? newUser = AuthView.SignUp();
        if (newUser != null)
        {
            _authService.Signup(newUser);
            WriteLine($"Registration successful. Welcome, {newUser.Username}!");
            WriteLine("-----------------------------------------");
            WriteLine();
            Thread.Sleep(2000);

            // Initialize the cart with the new user's ID
            _cartService = new CartService(_authService);

            // Initialize the order service with the authenticated user's ID
            _orderService = new OrderService(_authService);

            AfterLoginFlow();
        }
        else
        {
            WriteLine("Sign-up failed. Try again.");
            WriteLine("--------------");
            WriteLine();
            Thread.Sleep(2000);

        }
    }

    private void AfterLoginFlow()
    {
        while (true)
        {
            Clear();
            WriteLine("Welcome, " + _authService.CurrentUser!.Username + "!");
            WriteLine("------------------");
            WriteLine("1. View Products");
            WriteLine("2. View Cart");
            WriteLine("3. Order History");
            WriteLine("4. Logout");
            if (_authService.CurrentUser!.Username == "admin")
            {
                WriteLine("5. Admin Menu");
            }
            WriteLine("------------------");
            Write("Enter your choice: ");
            string? choice = ReadLine();
            WriteLine();
            switch (choice)
            {
                case "1":
                    Clear();
                    ProductView.DisplayProducts();
                    int productId = ProductView.SelectProduct();
                    while (productId != 0 && productId != -1)
                    {
                        try
                        {
                            _cartService!.AddToCart(productId);
                        }
                        catch (Exception e)
                        {

                            WriteLine($"Something went wrong: {e.Message}");
                            Thread.Sleep(2000);
                            Clear();
                            ProductView.DisplayProducts();
                            productId = ProductView.SelectProduct();
                            continue;
                        }
                        WriteLine($"Product ID: {productId} has been added to your cart.");
                        WriteLine("-------------------------------------------");
                        WriteLine();
                        Thread.Sleep(2000);
                        Clear();
                        ProductView.DisplayProducts();
                        productId = ProductView.SelectProduct();
                    }
                    break;
                case "2":
                    CartView.DisplayCart(_authService.CurrentUser!.ShoppingCart!);
                    break;
                case "3":
                    Clear();
                    var orders = _orderService!.GetAll();
                    OrderView.DisplayOrderHistory(orders);
                    break;
                case "4":
                    _authService.Logout();
                    return; // Exit to the main menu

                case "5":
                    AdminView.DisplayAdminMenu();
                    break;
                default:
                    WriteLine("Invalid choice.");
                    WriteLine("--------------");
                    WriteLine();
                    Thread.Sleep(1000);
                    break;
            }
        }
    }

}
