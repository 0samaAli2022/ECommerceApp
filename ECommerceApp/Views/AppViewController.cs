
using ECommerceApp.Application.Interfaces;
using ECommerceApp.Domain.Entities;


namespace ECommerceApp.Views;

public class AppViewController(IAuthService authService, ICartService cartService,
 CartView cartView, OrderView orderView, ProductView productView, AdminView adminView)
{
    private readonly IAuthService _authService = authService;
    private readonly ICartService _cartService = cartService;
    private readonly CartView _cartView = cartView;
    private readonly OrderView _orderView = orderView;
    private readonly ProductView _productView = productView;
    private readonly AdminView _adminView = adminView;

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
                    WriteLine("---------------");

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
                WriteLine("---------------");
                WriteLine();
                Thread.Sleep(2000);
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
            AfterLoginFlow();
        }
        else
        {
            WriteLine("Sign-up failed. Try again.");
            WriteLine("---------------------------");
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
            WriteLine("---------------------");
            WriteLine("1. View Products");
            WriteLine("2. View Cart");
            WriteLine("3. Order History");
            WriteLine("4. Logout");
            if (_authService.CurrentUser.Username == "admin")
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
                    _productView.DisplayProducts();
                    int productId = ProductView.SelectProduct();
                    while (productId != 0 && productId != -1)
                    {
                        try
                        {
                            _cartService.AddToCart(productId);
                        }
                        catch (Exception e)
                        {

                            WriteLine($"Something went wrong: {e.Message}");
                            Thread.Sleep(2000);
                            Clear();
                            _productView.DisplayProducts();
                            productId = ProductView.SelectProduct();
                            continue;
                        }
                        WriteLine($"Product ID: {productId} has been added to your cart.");
                        WriteLine("---------------------------------------------------");
                        WriteLine();
                        Thread.Sleep(2000);
                        Clear();
                        _productView.DisplayProducts();
                        productId = ProductView.SelectProduct();
                    }
                    break;
                case "2":
                    _cartView.DisplayCart();
                    break;
                case "3":
                    Clear();
                    _orderView.DisplayOrderHistory();
                    break;
                case "4":
                    _authService.Logout();
                    return; // Exit to the main menu

                case "5":
                    if (_authService.CurrentUser!.Username != "admin")
                    {
                        WriteLine("Invalid choice.");
                        WriteLine("----------------");
                        WriteLine();
                        Thread.Sleep(1000);
                        break;
                    }
                    _adminView.DisplayAdminMenu();
                    break;
                default:
                    WriteLine("Invalid choice.");
                    WriteLine("----------------");
                    WriteLine();
                    Thread.Sleep(1000);
                    break;
            }
        }
    }

}
