namespace MyProject.Controllers;
using MyProject.Models;
public class InventoryController
{
    Supermarket supermarket;
    public InventoryController()
    {
        supermarket = Supermarket.Instance;
    }

    public void AddItem(Item item)
    {
        supermarket.Inventory.AddItem(item);
    }

    public void Restock(string itemId, int quantity)
    {
        supermarket.Inventory.UpdateStock(itemId, quantity);
    }

    public List<Item> ShowAllItems()
    {
        return supermarket.Inventory.ListAllItems();
    }

    public List<Item> ShowNearExpiryItems(int daysToExpiry)
    {
        return supermarket.Inventory.GetNearExpiryItems(daysToExpiry);
    }

    public List<Item> ShowLowStockItems(int minimumStock)
    {
        return supermarket.Inventory.GetLowStockItems(minimumStock);
    }


    public List<Item>  GetAllItems()
    {
        return supermarket.Inventory.ListAllItems();
    }

    public Item GetItemById(string itemId)
    {
        return supermarket.Inventory.GetItem(itemId);
    }

    public List<Item> SearchItems(string keyword)
    {
        return supermarket.Inventory.SearchItems(keyword);
    }

    public void RemoveItem(string itemId)
    {
        supermarket.Inventory.RemoveItem(itemId);
    }
}