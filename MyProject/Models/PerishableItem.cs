namespace MyProject.Models;

public class PerishableItem : Item, IExpirable
{
    public DateTime ExpiryDate { get; set; }

    public PerishableItem()
    {
    }

    public PerishableItem(string itemId, string name, decimal sellPrice, decimal costPrice, int stockQuantity, Category category, DateTime expiryDate)
        : base(itemId, name, sellPrice, costPrice, stockQuantity, category)
    {
        ExpiryDate = expiryDate;
    }

    public bool IsExpired()
    {
        DateTime now = DateTime.Now;
        return now > ExpiryDate;
    }

    public int DaysUntilExpiry()
    {
        DateTime now = DateTime.Now;
        TimeSpan timeUntilExpiry = ExpiryDate - now;
        return (int)timeUntilExpiry.TotalDays;
    }

    public override string GetDisplayInformation()
    {
        string displayInformation = base.GetDisplayInformation();
        displayInformation += $"\nExpiry Date: {ExpiryDate}";
        return displayInformation;
    }
}
