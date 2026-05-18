using MyProject.Interface;

namespace MyProject.Models;

public class SalesRecord: IDisplayable
{
    public string RecordId { get; set; } = string.Empty;
    public string CustomerId { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public decimal Total { get; set; }
    public decimal DiscountAmount { get; set; }
    public List<CartItem> ItemsSold { get; set; } = new();

    public SalesRecord()
    {
    }

    public SalesRecord(string recordId, string customerId, DateTime timestamp, decimal total, decimal discountAmount, List<CartItem> itemsSold)
    {
        RecordId = recordId;
        CustomerId = customerId;
        Timestamp = timestamp;
        Total = total;
        DiscountAmount = discountAmount;
        ItemsSold = itemsSold;
    }

    public string GetDisplayInformation()
    {
        decimal subtotal = 0;
        int totalItemsSold = 0;
        decimal totalCost = 0;
        decimal grossProfit = 0;

        string displayInformation =
            $"Sales Record ID : {RecordId}\n" +
            $"Customer ID     : {CustomerId}\n" +
            $"Timestamp       : {Timestamp:dd/MM/yyyy HH:mm:ss}\n" +
            new string('-', 85) + "\n" +
            $"{"Item ID",-10} {"Name",-20} {"Sell Price",15} {"Qty",8} {"Subtotal",15}\n" +
            new string('-', 85) + "\n";

        foreach (CartItem cartItem in ItemsSold)
        {
            decimal itemSubtotal = cartItem.Item.SellPrice * cartItem.Quantity;
            decimal itemCost = cartItem.Item.CostPrice * cartItem.Quantity;
            decimal itemProfit = itemSubtotal - itemCost;

            subtotal += itemSubtotal;
            totalCost += itemCost;
            grossProfit += itemProfit;
            totalItemsSold += cartItem.Quantity;

            displayInformation +=
                $"{cartItem.Item.ItemId,-10} " +
                $"{cartItem.Item.Name,-20} " +
                $"{cartItem.Item.SellPrice,15:C} " +
                $"{cartItem.Quantity,8} " +
                $"{itemSubtotal,15:C}\n";
        }

        decimal netProfit = grossProfit - DiscountAmount;

        displayInformation +=
            new string('-', 85) + "\n" +
            $"{"Total Items Sold",-60} {totalItemsSold,22}\n" +
            $"{"Subtotal",-60} {subtotal,22:C}\n" +
            $"{"Discount",-60} {DiscountAmount,22:C}\n" +
            $"{"Final Total",-60} {Total,22:C}\n" +
            $"{"Total Cost",-60} {totalCost,22:C}\n" +
            $"{"Gross Profit",-60} {grossProfit,22:C}\n" +
            $"{"Net Profit After Discount",-60} {netProfit,22:C}\n";

        return displayInformation;
    }
}
