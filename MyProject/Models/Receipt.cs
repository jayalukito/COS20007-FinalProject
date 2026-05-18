namespace MyProject.Models;
using MyProject.Interface;

public class Receipt: IDisplayable
{
    public string ReceiptId { get; set; } = string.Empty;
    public string CustomerId { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public decimal Total { get; set; }
    public decimal DiscountAmount { get; set; }
    public List<CartItem> PurchasedItems { get; set; } = new();

    public Receipt()
    {
    }

    public Receipt(string receiptId, string customerId, DateTime timestamp, decimal total, decimal discountAmount, List<CartItem> purchasedItems)
    {
        ReceiptId = receiptId;
        CustomerId = customerId;
        Timestamp = timestamp;
        Total = total;
        DiscountAmount = discountAmount;
        PurchasedItems = purchasedItems;
    }

    public string GetDisplayInformation()
    {
        decimal subtotal = 0;

        string displayInformation =
            $"Receipt ID  : {ReceiptId}\n" +
            $"Customer ID : {CustomerId}\n" +
            $"Timestamp   : {Timestamp:dd/MM/yyyy HH:mm:ss}\n" +
            new string('-', 75) + "\n" +
            $"{"Item ID",-10} {"Name",-20} {"Price",15} {"Qty",8} {"Subtotal",15}\n" +
            new string('-', 75) + "\n";

        foreach (CartItem cartItem in PurchasedItems)
        {
            decimal itemSubtotal = cartItem.Item.SellPrice * cartItem.Quantity;
            subtotal += itemSubtotal;

            displayInformation +=
                $"{cartItem.Item.ItemId,-10} " +
                $"{cartItem.Item.Name,-20} " +
                $"{cartItem.Item.SellPrice,15:C} " +
                $"{cartItem.Quantity,8} " +
                $"{itemSubtotal,15:C}\n";
        }

        decimal finalTotal = subtotal - DiscountAmount;

        if (finalTotal < 0)
        {
            finalTotal = 0;
        }

        displayInformation +=
            new string('-', 75) + "\n" +
            $"{"Subtotal",-55} {subtotal,18:C}\n" +
            $"{"Discount",-55} {DiscountAmount,18:C}\n" +
            $"{"Final Total",-55} {finalTotal,18:C}\n";

        return displayInformation;
    }
}
