namespace MyProject.Controllers;

using MyProject.Controllers;
using MyProject.Models;
using MyProject.Views;

public class CheckoutMenu: BaseMenu
{
    private SalesController salesController = new SalesController();
    public CheckoutMenu(){}

    public override void Show()
    {
        Console.Clear();
        Console.WriteLine("=== Checkout ===");

        try
        {
            Receipt receipt = salesController.Checkout();

            Console.WriteLine("Checkout successful.");
            Console.WriteLine();
            Console.WriteLine(receipt.GetDisplayInformation());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Pause();

    }
}