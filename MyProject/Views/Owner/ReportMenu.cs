namespace MyProject.Views;

using MyProject.Controllers;
using MyProject.Models;
using MyProject.Utils;

public class ReportMenu : BaseMenu
{
    private SalesController salesController = new SalesController();
    private CategoryController categoryController = new CategoryController();

    public override void Show()
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.Clear();

            Console.WriteLine("=== Sales Report Menu ===");
            Console.WriteLine("1. Filter Sales Report by Category");
            Console.WriteLine("2. Filter Sales Report by Month");
            Console.WriteLine("3. Go Back");
            Console.Write("Enter your choice: ");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    ShowSalesReportByCategory();
                    break;

                case "2":
                    Console.Clear();
                    ShowSalesReportByDate();
                    break;

                case "3":
                    Console.Clear();
                    isRunning = false;
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Invalid choice.");
                    Pause();
                    break;
            }
        }
    }


    private void ShowSalesReportByCategory()
    {
        Console.Clear();
        Console.WriteLine("=== Sales Report by Category ===");

        List<Category> categories = categoryController.ViewCategories();

        if (categories.Count == 0)
        {
            Console.WriteLine("No categories available.");
            Pause();
            return;
        }

        Console.WriteLine("Available Categories:");

        foreach (Category category in categories)
        {
            Console.WriteLine(category.GetDisplayInformation());
        }

        Console.WriteLine();

        string categoryId = InputHelper.ReadRequiredString("Enter Category ID: ");

        try
        {
            SalesReport salesReport = salesController.GenerateReportByCategory(categoryId);
            Console.WriteLine(salesReport.GetDisplayInformation());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Pause();
    }

    private void ShowSalesReportByDate()
    {
        Console.Clear();
        Console.WriteLine("=== Sales Report by Date ===");

        int month = InputHelper.ReadRequiredInt("Enter Month (1-12): ");
        int year = InputHelper.ReadRequiredInt("Enter Year: ");

        try
        {
            SalesReport salesReport = salesController.GenerateReportByMonth(month, year);
            Console.WriteLine(salesReport.GetDisplayInformation());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Pause();
    }
}