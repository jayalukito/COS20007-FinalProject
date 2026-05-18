using MyProject.Controllers;
using MyProject.Models;

namespace MyProject.Views;

public class CustomerMenu:BaseMenu
{
    private CustomerController customerController { get; set; } = new();
    private SalesController salesController { get; set; } = new();
    private CheckoutMenu checkoutMenu = new();
    private ItemsMenu itemsMenu = new();
    private CartMenu cartMenu = new();   

    public CustomerMenu(){}
    public override void Show()
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.Clear();
            
            Console.WriteLine("=== Customer Menu ===");
            Console.WriteLine("1. View Items");
            Console.WriteLine("2. View Cart");
            Console.WriteLine("3. Checkout");
            Console.WriteLine("4. View Receipts");
            Console.WriteLine("5. Logout");
            Console.Write("Enter your choice: ");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    ShowItems();
                    break;

                case "2":
                    Console.Clear();
                    ShowCart();
                    break;

                case "3":
                    Console.Clear();
                    Checkout();
                    break;

                case "4":
                    Console.Clear();
                    ShowReceipts();
                    break;

                case "5":
                    Console.Clear();
                    customerController.Logout();
                    isRunning = false;
                    Console.WriteLine("Logout successful.");
                    Pause();
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Invalid choice.");
                    Pause();
                    break;
            }
        }
    }

    public void ShowItems()
    {
        Console.Clear();
        itemsMenu.Show();
        
    }

    public void Checkout()
    {
        Console.Clear();
        checkoutMenu.Show();
    }

    public void ShowCart()
    {
        Console.Clear();
        cartMenu.Show();
    }



    public void ShowReceipts()
    {
        Console.Clear();
        Console.WriteLine("=== Receipts ===");

        foreach (Receipt receipt in customerController.GetAllReceipts())
        {
            Console.WriteLine(receipt.GetDisplayInformation());
        }
       
       Pause();
    }

}
