namespace MyProject.Views;

using MyProject.Controllers;
using MyProject.Models;
using MyProject.Utils;

public class CategoryMenu : BaseMenu
{
    private readonly CategoryController categoryController = new();

    public CategoryMenu()
    {
    }

    public override void Show()
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.Clear();

            Console.WriteLine("=== Category Manager ===");
            Console.WriteLine("1. View Categories");
            Console.WriteLine("2. Add Category");
            Console.WriteLine("3. Remove Category");
            Console.WriteLine("4. Go Back");
            Console.Write("Enter your choice: ");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    ShowCategories();
                    break;

                case "2":
                    ShowAddCategory();
                    break;

                case "3":
                    ShowRemoveCategory();
                    break;

                case "4":
                    isRunning = false;
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    Pause();
                    break;
            }
        }
    }

    private void ShowCategories()
    {
        Console.Clear();
        Console.WriteLine("=== Categories ===");

        List<Category> categories = categoryController.ViewCategories();

        if (categories.Count == 0)
        {
            Console.WriteLine("No categories available.");
        }
        else
        {
            foreach (Category category in categories)
            {
                Console.WriteLine(category.GetDisplayInformation());
            }
        }

        Pause();
    }

    private void ShowAddCategory()
    {
        Console.Clear();
        Console.WriteLine("=== Add Category ===");

        string categoryName = InputHelper.ReadRequiredString("Category name: ");

        try
        {
            Category category = categoryController.AddCategory(categoryName);

            Console.WriteLine("Category added successfully.");
            Console.WriteLine(category.GetDisplayInformation());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Pause();
    }

    private void ShowRemoveCategory()
    {
        Console.Clear();
        Console.WriteLine("=== Remove Category ===");

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

        string categoryId = InputHelper.ReadRequiredString("Category ID to remove: ");

        try
        {
            categoryController.RemoveCategory(categoryId);
            Console.WriteLine("Category removed successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Pause();
    }
}