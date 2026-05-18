namespace MyProject.Views;

using MyProject.Controllers;
using MyProject.Models;
using MyProject.Utils;
using MyProject.Factories;

public class InventoryMenu: BaseMenu
{
    InventoryController inventoryController = new();
    Supermarket supermarket = Supermarket.Instance;
    public InventoryMenu(){}

    public override void Show()
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.Clear();
            Console.WriteLine("=== Inventory ===");
            Console.WriteLine("1. Add Item");
            Console.WriteLine("2. Remove Item");
            Console.WriteLine("3. Restock Item");
            Console.WriteLine("4. View All Items");
            Console.WriteLine("5. View Near Expiry Items");
            Console.WriteLine("6. View Low Stock Items");
            Console.WriteLine("7. Go Back");
            Console.Write("Enter your choice: ");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    ShowAddItem();
                    break;

                case "2":
                    ShowRemoveItem();
                    break;

                case "3":
                    ShowRestockItem();
                    break;

                case "4":
                    ShowAllItems();
                    break;

                case "5":
                    ShowNearExpiryItems();
                    break;

                case "6":
                    ShowLowStockItems();
                    break;
                case "7":
                    isRunning = false;
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    Pause();
                    break;
            }
        }
    }
    public void ShowRemoveItem()
    {
        Console.Clear();
        Console.WriteLine("=== Remove Item ===");

        Console.Write("Enter item ID: ");
        string itemId = Console.ReadLine() ?? string.Empty;

        try
        {
            supermarket.Inventory.RemoveItem(itemId);
            Console.WriteLine("Item removed successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Pause();
    }
        

    public void ShowRestockItem()
    {
        Console.Clear();
        Console.WriteLine("=== Restock Item ===");

        string itemId = InputHelper.ReadRequiredString("Enter Item ID:");

        try
        {
            int quantity = InputHelper.ReadRequiredInt("Enter Quantity:");
            
            supermarket.Inventory.UpdateStock(itemId,quantity);
            Console.WriteLine("Item restocked successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Pause();
    }

    public void ShowAllItems()
    {
        Console.Clear();
        Console.WriteLine("=== Inventory ===");

        List<Item> items = inventoryController.ShowAllItems();

        if (items.Count == 0)
        {
            Console.WriteLine("Inventory is empty.");
        }
        else
        {
            foreach (Item item in items)
            {
                Console.WriteLine(item.GetDisplayInformation());
            }
        }

        Pause();
    }

    public void ShowNearExpiryItems()
    {
        Console.Clear();
        Console.WriteLine("=== Near Expiry Items ===");

        int daysToExpiry = InputHelper.ReadRequiredInt("Enter days to expiry:");

        List<Item> nearExpiryItems = inventoryController.ShowNearExpiryItems(daysToExpiry);

        if (nearExpiryItems.Count == 0)
        {
            Console.WriteLine("No items near expiry.");
        }
        else
        {
            foreach (Item item in nearExpiryItems)
            {
                Console.WriteLine(item.GetDisplayInformation());
            }
        }

        Pause();
    }

    public void ShowLowStockItems()
    {
        Console.Clear();
        Console.WriteLine("=== Low Stock Items ===");

        int minimumStock = InputHelper.ReadRequiredInt("Enter minimum stock quantity:");

        List<Item> lowStockItems = inventoryController.ShowLowStockItems(minimumStock);
        if (lowStockItems.Count == 0)
        {
            Console.WriteLine("No low stock items.");
        }
        else
        {
            foreach (Item item in lowStockItems)
            {
                Console.WriteLine(item.GetDisplayInformation());
            }
        }

        Pause();
    }
    public void ShowAddItem()
    {
        Console.Clear();
        Console.WriteLine("=== Add Item ===");
        
        DisplayItemTypes();

        ItemType itemType = InputHelper.ReadRequiredItemType("Choose Item type:");
        string name = InputHelper.ReadRequiredString("Name:");
        decimal sellPrice = InputHelper.ReadRequiredDecimal("Sell Price:");
        decimal costPrice = InputHelper.ReadRequiredDecimal("Cost Price:");
        int stock = InputHelper.ReadRequiredInt("Stock Quantity:");
        Category? category = InputHelper.ReadRequiredCategory("Category:");

        try
        {
            Item item;

            if (itemType == ItemType.Perishable)
            {
                Console.WriteLine("ping");
                DateTime expiryDate = InputHelper.ReadRequiredDate("Expiry Date (yyyy-MM-dd):");

                if (expiryDate < DateTime.Now)
                {
                    Console.WriteLine("Invalid expiry date.");
                    Pause();
                    return;
                }

                item = ItemFactory.CreateItem(
                    ItemType.Perishable,
                    name,
                    sellPrice,
                    costPrice,
                    stock,
                    category,
                    expiryDate
                );

                inventoryController.AddItem(item);
                Console.WriteLine("Perishable item added successfully.");
            }
            else if (itemType == ItemType.Electronics)
            {
                string brand = InputHelper.ReadRequiredString("Brand:");
                int warrantyMonths = InputHelper.ReadRequiredInt("Warranty Months:");

                item = ItemFactory.CreateItem(
                    ItemType.Electronics,
                    name,
                    sellPrice,
                    costPrice,
                    stock,
                    category,
                    null,
                    warrantyMonths,
                    brand
                );

                inventoryController.AddItem(item);
                Console.WriteLine("Electronics item added successfully.");
            }
            else
            {
                Console.WriteLine("Invalid item type.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

       Pause();
    }


    private void DisplayItemTypes()
    {
        foreach (ItemType itemType in Enum.GetValues(typeof(ItemType)))
        {
            Console.WriteLine($"{(int)itemType}. {itemType} Item");
        }
    }
}