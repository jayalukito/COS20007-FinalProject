namespace MyProject.Views;

using MyProject.Controllers;
using MyProject.Models;

public class LoginMenu: BaseMenu
{
    private readonly AuthController authController;

    public LoginMenu(AuthController authController)
    {
        this.authController = authController;
    }

    public override void Show()
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.Clear();

            Console.WriteLine("=== Supermarket System ===");
            Console.WriteLine("1. Customer Login");
            Console.WriteLine("2. Customer Register");
            Console.WriteLine("3. Owner Login");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    CustomerLogin();
                    break;

                case "2":
                    CustomerRegister();
                    break;

                case "3":
                    OwnerLogin();
                    break;

                case "4":
                    isRunning = false;
                    Console.WriteLine("Thank you for using the system.");
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    Pause();
                    break;
            }
        }
    }

    private void CustomerLogin()
    {
        Console.Clear();
        Console.WriteLine("=== Customer Login ===");

        Console.Write("Username: ");
        string username = Console.ReadLine() ?? string.Empty;

        Console.Write("Password: ");
        string password = Console.ReadLine() ?? string.Empty;

        try
        {
            Customer customer = authController.LoginCustomer(username, password);

            Console.WriteLine();
            Console.WriteLine($"Login successful. Welcome, {customer.Name}!");

            CustomerMenu customerMenu = new CustomerMenu();
            customerMenu.Show();

            Pause();
        }
        catch (Exception ex)
        {
            Console.WriteLine();
            Console.WriteLine(ex.Message);
            Pause();
        }
    }

    private void CustomerRegister()
    {
        Console.Clear();
        Console.WriteLine("=== Customer Register ===");

        Console.Write("Name: ");
        string name = Console.ReadLine() ?? string.Empty;

        Console.Write("Username: ");
        string username = Console.ReadLine() ?? string.Empty;

        Console.Write("Password: ");
        string password = Console.ReadLine() ?? string.Empty;

        try
        {
            Customer customer = authController.RegisterCustomer(name, username, password);

            Console.WriteLine();
            Console.WriteLine($"Registration successful. Welcome, {customer.Name}!");

            Pause();
        }
        catch (Exception ex)
        {
            Console.WriteLine();
            Console.WriteLine(ex.Message);
            Pause();
        }
    }

    private void OwnerLogin()
    {
        Console.Clear();
        Console.WriteLine("=== Owner Login ===");

        Console.Write("Username: ");
        string username = Console.ReadLine() ?? string.Empty;

        Console.Write("Password: ");
        string password = Console.ReadLine() ?? string.Empty;

        try
        {
            Owner owner = authController.LoginOwner(username, password);

            Console.WriteLine();
            Console.WriteLine($"Owner login successful. Welcome, {owner.Name}!");

            OwnerMenu ownerMenu = new OwnerMenu();
            ownerMenu.Show();

            Pause();
        }
        catch (Exception ex)
        {
            Console.WriteLine();
            Console.WriteLine(ex.Message);
            Pause();
        }
    }


}