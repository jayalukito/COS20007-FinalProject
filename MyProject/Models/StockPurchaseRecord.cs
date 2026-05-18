namespace MyProject.Models;

public class StockPurchaseRecord
{
    public string RecordId { get; set; } = string.Empty;
    public string ItemId { get; set; } = string.Empty;
    public string ItemName { get; set; } = string.Empty;
    public int QuantityPurchased { get; set; }
    public decimal CostPerUnit { get; set; }
    public DateTime Timestamp { get; set; }

    public StockPurchaseRecord()
    {
    }

    public StockPurchaseRecord(string recordId, string itemId, string itemName, int quantityPurchased, decimal costPerUnit, DateTime timestamp)
    {
    }

    public decimal GetTotalCost()
    {
        throw new NotImplementedException();
    }
}
