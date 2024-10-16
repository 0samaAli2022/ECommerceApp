
namespace ECommerceApp.Views;

public static class AdminView
{
    public static void DisplayAdminMenu()
    {
        Clear();
        WriteLine("Admin Menu");
        WriteLine("-------------");
        WriteLine("1. View products");
        WriteLine("2. Main menu");
        WriteLine("-------------");
        Write("Enter your choice: ");
        string? choice = ReadLine();
        WriteLine();
        if (String.IsNullOrEmpty(choice))
        {
            WriteLine("Invalid input. Try again.");
            Thread.Sleep(2000);
            return;
        }

        switch (choice)
        {
            case "1":
                Clear();
                ProductView.DisplayProducts();
                WriteLine("1. Add product");
                WriteLine("2. Update product");
                WriteLine("3. Remove product");
                WriteLine("-------------");
                Write("Enter your choice: ");
                choice = ReadLine();
                WriteLine();
                if (String.IsNullOrEmpty(choice))
                {
                    WriteLine("Invalid input. Try again.");
                    Thread.Sleep(2000);
                    return;
                }

                switch (choice)
                {
                    case "1":
                        ProductView.AddProduct();
                        break;
                    case "2":
                        ProductView.UpdateProduct();
                        break;
                    case "3":
                        ProductView.RemoveProduct();
                        break;
                    default:
                        WriteLine("Invalid input. Try again.");
                        Thread.Sleep(2000);
                        DisplayAdminMenu();
                        break;

                }
                break;
        }
    }
}
