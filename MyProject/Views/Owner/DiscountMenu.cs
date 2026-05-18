namespace MyProject.Views;

using MyProject.Controllers;
using MyProject.Interface;
using MyProject.Models;
using MyProject.Utils;

public class DiscountMenu : BaseMenu
{
    private readonly DiscountController discountController = new();

    public DiscountMenu()
    {
    }

    public override void Show()
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.Clear();

            Console.WriteLine("=== Discount Manager ===");
            Console.WriteLine("1. View Discount Rules");
            Console.WriteLine("2. Add Expiry Discount Rule");
            Console.WriteLine("3. Add Purchase Total Discount Rule");
            Console.WriteLine("4. Activate Discount Rule");
            Console.WriteLine("5. Deactivate Discount Rule");
            Console.WriteLine("6. Remove Discount Rule");
            Console.WriteLine("7. Go Back");
            Console.Write("Enter your choice: ");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    ShowDiscountRules();
                    break;

                case "2":
                    ShowAddExpiryDiscountRule();
                    break;

                case "3":
                    ShowAddPurchaseTotalDiscountRule();
                    break;

                case "4":
                    ShowActivateDiscountRule();
                    break;

                case "5":
                    ShowDeactivateDiscountRule();
                    break;

                case "6":
                    ShowRemoveDiscountRule();
                    break;

                case "7":
                    isRunning = false;
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    Pause();
                    break;
            }
        }
    }

    private void ShowDiscountRules()
    {
        Console.Clear();
        Console.WriteLine("=== Discount Rules ===");

        List<DiscountRule> discountRules = discountController.GetAllDiscountRules();

        if (discountRules.Count == 0)
        {
            Console.WriteLine("No discount rules available.");
        }
        else
        {
            foreach (DiscountRule rule in discountRules)
            {
               
                Console.WriteLine(rule.GetDisplayInformation());
                Console.WriteLine("----------------------------------------");
            }
        }

        Pause();
    }

    private void ShowAddExpiryDiscountRule()
    {
        Console.Clear();
        Console.WriteLine("=== Add Expiry Discount Rule ===");

        string ruleId = discountController.GenerateDiscountRuleId();
        string name = InputHelper.ReadRequiredString("Rule name: ");
        int daysThreshold = InputHelper.ReadRequiredInt("Days threshold: ");
        decimal discountPercent = InputHelper.ReadRequiredDecimal("Discount percent: ");

        try
        {
            ExpiryDiscountRule discountRule = new ExpiryDiscountRule(
                ruleId,
                name,
                true,
                daysThreshold,
                discountPercent
            );

            discountController.AddDiscountRule(discountRule);

            Console.WriteLine("Expiry discount rule added successfully.");
            Console.WriteLine($"Generated Rule ID: {ruleId}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Pause();
    }

    private void ShowAddPurchaseTotalDiscountRule()
    {
        Console.Clear();
        Console.WriteLine("=== Add Purchase Total Discount Rule ===");

        string ruleId = discountController.GenerateDiscountRuleId();
        string name = InputHelper.ReadRequiredString("Rule name: ");
        decimal minimumPurchase = InputHelper.ReadRequiredDecimal("Minimum purchase: ");
        decimal discountPercent = InputHelper.ReadRequiredDecimal("Discount percent: ");

        try
        {
            PurchaseTotalDiscountRule discountRule = new PurchaseTotalDiscountRule(
                ruleId,
                name,
                true,
                minimumPurchase,
                discountPercent
            );

            discountController.AddDiscountRule(discountRule);

            Console.WriteLine("Purchase total discount rule added successfully.");
            Console.WriteLine($"Generated Rule ID: {ruleId}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Pause();
    }

    private void ShowActivateDiscountRule()
    {
        Console.Clear();
        Console.WriteLine("=== Activate Discount Rule ===");

        ShowDiscountRulesWithoutPause();

        string ruleId = InputHelper.ReadRequiredString("Rule ID to activate: ");

        try
        {
            discountController.ActivateDiscountRule(ruleId);
            Console.WriteLine("Discount rule activated successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Pause();
    }

    private void ShowDeactivateDiscountRule()
    {
        Console.Clear();
        Console.WriteLine("=== Deactivate Discount Rule ===");

        ShowDiscountRulesWithoutPause();

        string ruleId = InputHelper.ReadRequiredString("Rule ID to deactivate: ");

        try
        {
            discountController.DeactivateDiscountRule(ruleId);
            Console.WriteLine("Discount rule deactivated successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Pause();
    }

    private void ShowRemoveDiscountRule()
    {
        Console.Clear();
        Console.WriteLine("=== Remove Discount Rule ===");

        ShowDiscountRulesWithoutPause();

        string ruleId = InputHelper.ReadRequiredString("Rule ID to remove: ");

        try
        {
            discountController.RemoveDiscountRule(ruleId);
            Console.WriteLine("Discount rule removed successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Pause();
    }

    private void ShowDiscountRulesWithoutPause()
    {
        List<DiscountRule> discountRules = discountController.GetAllDiscountRules();

        if (discountRules.Count == 0)
        {
            Console.WriteLine("No discount rules available.");
            Console.WriteLine();
            return;
        }

        foreach (DiscountRule rule in discountRules)
        {
            Console.WriteLine($"{rule.RuleId} - {rule.Name} - {(rule.IsActive ? "Active" : "Inactive")}");
        }

        Console.WriteLine();
    }
}