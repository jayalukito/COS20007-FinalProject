namespace MyProject.Controllers;

using MyProject.Models;

public class CategoryController
{
    private readonly Supermarket supermarket;

    public CategoryController()
    {
        supermarket = Supermarket.Instance;
    }

    public List<Category> ViewCategories()
    {
        return supermarket.Categories;
    }

    public Category AddCategory(string categoryName)
    {
        if (string.IsNullOrWhiteSpace(categoryName))
        {
            throw new Exception("Category name cannot be empty.");
        }

        bool categoryExists = supermarket.Categories.Any(category =>
            category.Name.Equals(categoryName.Trim(), StringComparison.OrdinalIgnoreCase)
        );

        if (categoryExists)
        {
            throw new Exception("Category already exists.");
        }

        string categoryId = GenerateCategoryId();

        Category category = new Category(categoryId, categoryName.Trim());

        supermarket.Categories.Add(category);

        return category;
    }

    public void RemoveCategory(string categoryId)
    {
        if (string.IsNullOrWhiteSpace(categoryId))
        {
            throw new Exception("Category ID cannot be empty.");
        }

        Category? category = GetCategoryById(categoryId);

        if (category == null)
        {
            throw new Exception("Category not found.");
        }

        bool isCategoryUsed = false;

        foreach (Item item in supermarket.Inventory.Items)
        {
            if (item.Category.CategoryId.Equals(categoryId.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                isCategoryUsed = true;
                break;
            }
        }

        if (isCategoryUsed)
        {
            throw new Exception("Category cannot be removed because it is still used by one or more items.");
        }

        supermarket.Categories.Remove(category);
    }

    public Category? GetCategoryById(string categoryId)
    {
        return supermarket.Categories.FirstOrDefault(category =>
            category.CategoryId.Equals(categoryId.Trim(), StringComparison.OrdinalIgnoreCase)
        );
    }

    public Category? GetCategoryByName(string categoryName)
    {
        return supermarket.Categories.FirstOrDefault(category =>
            category.Name.Equals(categoryName.Trim(), StringComparison.OrdinalIgnoreCase)
        );
    }

    private string GenerateCategoryId()
    {
        int highestNumber = 0;

        foreach (Category category in supermarket.Categories)
        {
            string numberText = category.CategoryId.Replace("CAT", "");

            if (int.TryParse(numberText, out int number))
            {
                if (number > highestNumber)
                {
                    highestNumber = number;
                }
            }
        }

        int newNumber = highestNumber + 1;

        return "CAT" + newNumber.ToString("D3");
    }
}