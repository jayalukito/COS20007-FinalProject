using MyProject.Models;

namespace MyProject.Controllers;

public class CustomerController
{

    private Supermarket supermarket = Supermarket.Instance;

    public CustomerController()
    {
    }

    public CustomerController(Inventory inventory, Customer customer, SalesReport salesReport, List<DiscountRule> discountRules)
    {
    }


    public void Logout()
    {
        Supermarket instance = Supermarket.Instance;
        instance.Customer = null;
    }

    
    public List<Receipt> GetAllReceipts()
    {
        return supermarket.Customer.Receipts;
    }
}
