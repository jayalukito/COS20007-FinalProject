using System.Runtime.CompilerServices;

namespace MyProject.Models;

public class Inventory
{
    public List<Item> Items { get; set; } = new();
   

    public Inventory()
    {
    }


     public void AddItem(Item item)
    {
        if (item == null)
        {
            throw new Exception("Item cannot be null.");
        }

        item.ItemId = GenerateItemId(item);

        Items.Add(item);
    }

    public void RemoveItem(string itemId)
    {
        Items.Remove(Items.Find(item => item.ItemId == itemId));
    }

    public Item? GetItem(string itemId)
    {
        return Items.Find(item => item.ItemId == itemId);
    }

    public void UpdateStock(string itemId, int quantity)
    {
        var item = Items.Find(item => item.ItemId == itemId);
        item?.AddStock(quantity);
    }

    public List<Item> ListAllItems()
    {
        return Items;
    }

    public List<Item> GetLowStockItems(int minimumStock)
    {
        List<Item> items;
        items = Items.FindAll(item => item.StockQuantity < minimumStock);
        return items;
    }

    public List<Item> GetNearExpiryItems(int daysToExpiry)
    {
        List<Item> nearExpiryItems = new List<Item>();

        foreach (Item item in Items)
        {
            if (item is IExpirable expirableItem)
            {
                int daysLeft = expirableItem.DaysUntilExpiry();

                if (daysLeft >= 0 && daysLeft <= daysToExpiry)
                {
                    nearExpiryItems.Add(item);
                }
            }
        }

        return nearExpiryItems;
    }

    public List<Item> GetExpiredItems()
    {
        List<Item> items = new List<Item>();

        foreach(Item item in Items)
        {
            if(item is IExpirable perishableItem)
            {
                if(perishableItem.IsExpired())
                {
                    items.Add(item);
                }
            }
        }
       
        return items;
    }


    private string GenerateItemId(Item item)
    {
        string prefix;

        if (item is PerishableItem)
        {
            prefix = "P";
        }
        else if (item is ElectronicsItem)
        {
            prefix = "E";
        }
        else if (item is NonPerishableItem)
        {
            prefix = "N";
        }
        else
        {
            prefix = "I";
        }

        int highestNumber = 0;

        foreach (Item existingItem in Items)
        {
            string itemId = existingItem.ItemId;

            if (itemId.StartsWith(prefix))
            {
                string numberText = itemId.Replace(prefix, "");

                bool isNumber = int.TryParse(numberText, out int number);

                if (isNumber)
                {
                    if (number > highestNumber)
                    {
                        highestNumber = number;
                    }
                }
            }
        }

        int newNumber = highestNumber + 1;

        return $"{prefix}{newNumber.ToString("D3")}";
    }

    public List<Item> SearchItems(string keyword)
    {
        List<Item> searchResults = new List<Item>();

        if (string.IsNullOrWhiteSpace(keyword))
        {
            return searchResults;
        }

        string cleanedKeyword = keyword.Trim().ToLower();

        foreach (Item item in Items)
        {
            bool isBrandMatch = false;
            if (item is ElectronicsItem electronicsItem)
            {
                string brand = electronicsItem.Brand.ToLower();
                isBrandMatch = brand.Contains(cleanedKeyword);
            }

            string itemName = item.Name.ToLower();
            string itemId = item.ItemId.ToLower();
            string categoryName = item.Category.Name.ToLower();

            bool isNameMatch = itemName.Contains(cleanedKeyword);
            bool isIdMatch = itemId.Contains(cleanedKeyword);
            bool isCategoryMatch = categoryName.Contains(cleanedKeyword);

            if (isNameMatch || isIdMatch || isCategoryMatch || isBrandMatch)
            {
                searchResults.Add(item);
            }
        }

        return searchResults;
    } 

    public bool StockChecker (Item selectedItem, int quantity)
    {

        Item temp = Items.Find(item => item.ItemId.Equals(selectedItem.ItemId, StringComparison.OrdinalIgnoreCase));

        if(selectedItem.StockQuantity < quantity)
        {
            return false;
        }

        return true;
    }
}
