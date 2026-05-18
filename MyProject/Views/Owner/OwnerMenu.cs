
using MyProject.Controllers;
using MyProject.Models;
using MyProject.Utils;
namespace MyProject.Views;

public class OwnerMenu: BaseMenu
{
   
    private OwnerController ownerController = new();

    private InventoryMenu inventoryMenu = new();
    private CategoryMenu categoryMenu = new();

    private DiscountMenu discountMenu = new();

    private ReportMenu reportMenu = new();

    public OwnerMenu()
    {
    }

    public OwnerMenu(OwnerController controller)
    {
        ownerController = controller;
    }

    public override void Show()
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.Clear();

            Console.WriteLine("=== Owner Menu ===");
            Console.WriteLine("1. Inventory Manager");
            Console.WriteLine("2. View Receipts");
            Console.WriteLine("3. Category Manager");
            Console.WriteLine("4. Discount Manager");
            Console.WriteLine("5. Generate Sales Report");
            Console.WriteLine("6. Logout");
            Console.Write("Enter your choice: ");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    ShowInventoryManager();
                    break;

                case "2":
                    Console.Clear();
                    ShowSalesRecord();
                    break;

                case "3":
                    Console.Clear();
                    ShowCategoryManager();
                    break;

                case "4":
                    Console.Clear();
                    ShowDiscountManager();
                    break;

                case "5":
                    Console.Clear();
                    ShowSalesReport();
                    break;

                case "6":
                    Console.Clear();
                    ownerController.Logout();
                    isRunning = false;
                    Console.WriteLine("Logout successful.");
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Invalid choice.");
                    Pause();
                    break;
            }
        }
    }

    private void ShowSalesReport()
    {
        reportMenu.Show();
    }

    private void ShowInventoryManager()
    {
        inventoryMenu.Show();
    }

    private void ShowDiscountManager()
    {
        discountMenu.Show();
    }
    private void ShowCategoryManager()
    {
        categoryMenu.Show();
    }

    private void ShowSalesRecord()
    {
        Console.Clear();
        Console.WriteLine("=== Sales Records ===");
        SalesController salesController = new SalesController();
        List<SalesRecord> salesRecords = salesController.GetSalesRecords();
        foreach (SalesRecord salesRecord in salesRecords)
        {
            Console.WriteLine(salesRecord.GetDisplayInformation());
        }
        Pause();
    }
  
}