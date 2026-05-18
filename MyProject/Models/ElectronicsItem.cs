namespace MyProject.Models;

public class ElectronicsItem : Item
{
    public int WarrantyMonths { get; set; }
    public string Brand { get; set; } = string.Empty;

    public ElectronicsItem()
    {
    }

    public ElectronicsItem(string itemId, string name, decimal sellPrice, decimal costPrice, int stockQuantity, Category category, int warrantyMonths, string brand)
        : base(itemId, name, sellPrice, costPrice, stockQuantity, category)
    {
        WarrantyMonths = warrantyMonths;
        Brand = brand;
    }

    public override string GetDisplayInformation()
    {
        string displayInformation = base.GetDisplayInformation();
        displayInformation += $"\nWarranty Months: {WarrantyMonths}\nBrand: {Brand}";
        return displayInformation;
    }

}
