using MyProject.Models;

namespace MyProject.Controllers;

public class CustomerController
{

    private Supermarket supermarket = Supermarket.Instance;

    public CustomerController()
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
