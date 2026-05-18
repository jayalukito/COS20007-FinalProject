namespace MyProject.Views;

using MyProject.Controllers;
using MyProject.Models;
using MyProject.Utils;

public class ItemsMenu : BaseMenu
{
    private InventoryController inventoryController = new();
    private CartController cartController = new();

    public override void Show()
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.Clear();
            Console.WriteLine("=== Items Menu ===");
            Console.WriteLine("1. View All Items");
            Console.WriteLine("2. Add Item to Cart");
            Console.WriteLine("3. Search for an Item");
            Console.WriteLine("4. Go Back");
            Console.Write("Enter your choice: ");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    ShowAllItems();
                    break;

                case "2":
                    Console.Clear();
                    AddItemToCart();
                    break;

                case "3":
                    Console.Clear();
                    SearchItem();
                    break;

                case "4":
                    Console.Clear();
                    isRunning = false;
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Invalid choice. Please try again.");
                    Pause();
                    break;
            }
        }
    }

    public void ShowAllItems()
    {
        Console.Clear();
        Console.WriteLine("=== All Items ===");

        List<Item> items = inventoryController.GetAllItems();

        if (items.Count == 0)
        {
            Console.WriteLine("No items available.");
            Pause();
            return;
        }

        foreach (Item item in items)
        {
            Console.WriteLine(item.GetDisplayInformation());
            Console.WriteLine("----------------------------------------");
        }

        Pause();
    }

    public void AddItemToCart()
    {
        Console.Clear();
        Console.WriteLine("=== Add Item to Cart ===");

        List<Item> items = inventoryController.GetAllItems();

        if (items.Count == 0)
        {
            Console.WriteLine("No items available.");
            Pause();
            return;
        }

        Console.WriteLine("Available Items:");

        foreach (Item item in items)
        {
            Console.WriteLine($"{item.ItemId} - {item.Name} - {item.SellPrice:C} - Stock: {item.StockQuantity}");
        }

        Console.WriteLine();

        string itemId = InputHelper.ReadRequiredString("Enter Item ID: ");
        int quantity = InputHelper.ReadRequiredInt("Enter quantity: ");

        try
        {
            Item? item = inventoryController.GetItemById(itemId);

            if (item == null)
            {
                Console.WriteLine("Item not found.");
                Pause();
                return;
            }

            cartController.AddItemToCart(item, quantity);

            Console.WriteLine("Item added to cart successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Pause();
    }

    public void SearchItem()
    {
        Console.Clear();
        Console.WriteLine("=== Search Item ===");

        string keyword = InputHelper.ReadRequiredString("Enter item name/category keyword: ");

        List<Item> items = inventoryController.SearchItems(keyword);

        if (items.Count == 0)
        {
            Console.WriteLine("No item found.");
            Pause();
            return;
        }

        foreach (Item item in items)
        {
            Console.WriteLine(item.GetDisplayInformation());
            Console.WriteLine("----------------------------------------");
        }

        Pause();
    }
}