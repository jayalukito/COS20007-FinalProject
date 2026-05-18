namespace MyProject.Factories;

using MyProject.Models;

public static class ItemFactory
{
    public static Item CreateItem(
        ItemType itemType,
        string name,
        decimal sellPrice,
        decimal costPrice,
        int stockQuantity,
        Category category,
        DateTime? expiryDate = null,
        int? warrantyMonths = null,
        string? brand = null
    )
    {
        if (itemType == ItemType.Perishable)
        {
            if (expiryDate == null)
            {
                throw new Exception("Expiry date is required for perishable item.");
            }

            return new PerishableItem(
                string.Empty,
                name,
                sellPrice,
                costPrice,
                stockQuantity,
                category,
                expiryDate.Value
            );
        }

        if (itemType == ItemType.Electronics)
        {
            if (string.IsNullOrWhiteSpace(brand))
            {
                throw new Exception("Brand is required for electronics item.");
            }

            if (warrantyMonths == null)
            {
                throw new Exception("Warranty months is required for electronics item.");
            }

            return new ElectronicsItem(
                string.Empty,
                name,
                sellPrice,
                costPrice,
                stockQuantity,
                category,
                warrantyMonths.Value,
                brand
            );
        }

        throw new Exception("Invalid item type.");
    }
}