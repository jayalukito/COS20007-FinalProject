using MyProject.Models;
namespace MyProject.Utils
{
    public static class InputHelper
    {
        public static string ReadRequiredString(string label)
        {
            while (true)
            {
                Console.Write(label);
                string input = Console.ReadLine() ?? string.Empty;

                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input.Trim();
                }

                Console.WriteLine($"{label.Replace(":", "")} cannot be empty. Please input again.");
            }
        }

        public static decimal ReadRequiredDecimal(string label)
        {
            while (true)
            {
                Console.Write(label);
                string input = Console.ReadLine() ?? string.Empty;

                if (decimal.TryParse(input, out decimal value) && value > 0)
                {
                    return value;
                }

                Console.WriteLine($"{label.Replace(":", "")} must be a valid positive number. Please input again.");
            }
        }

        public static int ReadRequiredInt(string label)
        {
            while (true)
            {
                Console.Write(label);
                string input = Console.ReadLine() ?? string.Empty;

                if (int.TryParse(input, out int value) && value > 0)
                {
                    return value;
                }

                Console.WriteLine($"{label.Replace(":", "")} must be a valid positive number. Please input again.");
            }
        }

        public static DateTime ReadRequiredDate(string label)
        {
            while (true)
            {
                Console.Write(label);
                string input = Console.ReadLine() ?? string.Empty;

                if (DateTime.TryParse(input, out DateTime value))
                {
                    return value;
                }

                Console.WriteLine($"{label.Replace(":", "")} must be a valid date. Please input again.");
            }
        }

        public static Category ReadRequiredCategory(string label)
        {
            Supermarket supermarket = Supermarket.Instance;

            while (true)
            {
                Console.WriteLine("Available Categories:");

                foreach (Category category in supermarket.Categories)
                {
                    Console.WriteLine($"{category.CategoryId} - {category.Name}");
                }

                Console.Write(label);
                string input = Console.ReadLine() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Category cannot be empty. Please input again.");
                    continue;
                }

                Category? selectedCategory = null;
                
                foreach (Category category in supermarket.Categories)
                {
                    if (category.CategoryId.Equals(input.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        selectedCategory = category;
                        break;
                    }
                }

                if (selectedCategory != null)
                {
                    return selectedCategory;
                }

                Console.WriteLine("Category not found. Please choose an existing category.");
                Console.WriteLine();
            }
        }

        public static ItemType ReadRequiredItemType(string label)
        {
            while (true)
            {

                Console.Write(label);
                string input = Console.ReadLine() ?? string.Empty;

                if (int.TryParse(input, out int itemTypeNumber))
                {
                    if (Enum.IsDefined(typeof(ItemType), itemTypeNumber))
                    {
                        return (ItemType)itemTypeNumber;
                    }
                }

                Console.WriteLine("Invalid item type. Please input again.");
            }
        }

    }
}