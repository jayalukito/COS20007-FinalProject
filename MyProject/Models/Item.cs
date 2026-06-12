namespace MyProject.Models;

public abstract class Item
{
    public string ItemId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public decimal CostPrice { get; set; }
    public decimal SellPrice { get; set; }
    public int StockQuantity { get; set; }

    public Category Category { get; set; }

    public Item()
    {
    }

    public Item(string itemId, string name, decimal sellPrice, decimal costPrice,int stockQuantity, Category category)
    {
        ItemId =itemId;
        Name = name;
        SellPrice= sellPrice;
        CostPrice = costPrice;
        StockQuantity = stockQuantity;
        Category = category;
    }

    public virtual string GetDisplayInformation()
    {
        string displayInformation = $"Item ID: {ItemId}\nName: {Name}\nSellPrice: {SellPrice}\nCostPrice: {CostPrice}\nStock Quantity: {StockQuantity}\n";
        return displayInformation;
    }


    public virtual bool IsInStock()
    {
        return StockQuantity > 0;
    }

    public virtual void AddStock(int quantity)
    {
        StockQuantity += quantity;
    }

    public virtual void ReduceStock(int quantity)
    {
        if(quantity <= StockQuantity)
        {
            StockQuantity -= quantity;
        }
    }

    public virtual void UpdatePrice(decimal costPrice, decimal sellPrice)
    {
        CostPrice = costPrice;
        SellPrice = sellPrice;
    }
}
