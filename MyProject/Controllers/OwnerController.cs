using MyProject.Models;

namespace MyProject.Controllers;

public class OwnerController
{
    public OwnerController()
    {
    }

    public OwnerController(Owner owner)
    {
    }

    public List<Item> ViewStockReport()
    {
        throw new NotImplementedException();
    }

    public List<Item> ViewLowStock(int minimumStock)
    {
        throw new NotImplementedException();
    }

    public string ViewSalesReport(DateTime startDate, DateTime endDate)
    {
        throw new NotImplementedException();
    }

    public List<DiscountRule> ViewDiscountRules()
    {
        throw new NotImplementedException();
    }

    public List<StockPurchaseRecord> ViewStockPurchaseRecords()
    {
        throw new NotImplementedException();
    }

    public void Logout()
    {
        Supermarket instance = Supermarket.Instance;
        instance.Owner = null;
    }
}
