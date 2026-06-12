namespace MyProject.Models;

using MyProject.Interface;

public class SalesReport : IDisplayable
{
    public DateTime GeneratedAt { get; set; } = DateTime.Now;
    public List<SalesRecord> SalesRecords { get; set; } = new();


    public SalesReport(List<SalesRecord> salesRecords)
    {
        SalesRecords = salesRecords;
        GeneratedAt = DateTime.Now;
    }

    public SalesReport FilterByCategory(string categoryId)
    {
        List<SalesRecord> filteredRecords = new List<SalesRecord>();

        foreach (SalesRecord record in SalesRecords)
        {
            bool hasMatchingCategory = false;

            foreach (CartItem cartItem in record.ItemsSold)
            {
                if (cartItem.Item.Category.CategoryId.Equals(categoryId.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    hasMatchingCategory = true;
                    break;
                }
            }

            if (hasMatchingCategory)
            {
                filteredRecords.Add(record);
            }
        }

        if (filteredRecords.Count == 0)
        {
            throw new Exception("No sales records found for the specified category.");
        }

        return new SalesReport(filteredRecords);
    }

    public SalesReport FilterByMonth(int month, int year)
    {
        List<SalesRecord> filteredRecords = new List<SalesRecord>();


        foreach (SalesRecord record in SalesRecords)
        {
            Console.WriteLine(record.Timestamp.Month);
            if (record.Timestamp.Month == month && record.Timestamp.Year == year)
            {
                Console.WriteLine($"Month: {record.Timestamp.Month}, Year: {year}========");
                filteredRecords.Add(record);
            }
        }

        return new SalesReport(filteredRecords);
    }
    public decimal GetTotalRevenue()
    {
        decimal totalRevenue = 0;

        foreach (SalesRecord record in SalesRecords)
        {
            totalRevenue += record.Total;
        }

        return totalRevenue;
    }

    public decimal GetTotalDiscount()
    {
        decimal totalDiscount = 0;

        foreach (SalesRecord record in SalesRecords)
        {
            totalDiscount += record.DiscountAmount;
        }

        return totalDiscount;
    }

    public int GetTotalTransactions()
    {
        return SalesRecords.Count;
    }

    public int GetTotalItemsSold()
    {
        int totalItemsSold = 0;

        foreach (SalesRecord record in SalesRecords)
        {
            foreach (CartItem cartItem in record.ItemsSold)
            {
                totalItemsSold += cartItem.Quantity;
            }
        }

        return totalItemsSold;
    }

    public string GetDisplayInformation()
    {
        string displayInformation =
            $"=== Sales Report ===\n" +
            $"Generated At       : {GeneratedAt:dd/MM/yyyy HH:mm:ss}\n" +
            $"Total Transactions : {GetTotalTransactions()}\n" +
            $"Total Items Sold   : {GetTotalItemsSold()}\n" +
            $"Total Revenue      : {GetTotalRevenue():C}\n" +
            $"Total Discount     : {GetTotalDiscount():C}\n" +
            "------------------------------------------------------------\n";

        if (SalesRecords.Count == 0)
        {
            displayInformation += "No sales records found.\n";
            return displayInformation;
        }

        foreach (SalesRecord record in SalesRecords)
        {
            displayInformation += record.GetDisplayInformation();
            displayInformation += "\n------------------------------------------------------------\n";
        }

        return displayInformation;
    }
}