namespace MyProject.Factories;
using MyProject.Enums;
using MyProject.Models;
using System;

public static class DiscountFactory
{
    private static Supermarket supermarket = Supermarket.Instance;
    public static DiscountRule CreateDiscount(
        string name,
        bool isActive,
        DiscountType discountType,
        decimal discountPercent,
        int? daysThreshold = null,
        decimal? minimumAmount = null
    )
    {
        if (discountPercent <= 0)
        {
            throw new Exception("Discount percent must be greater than zero.");
        }
        if (discountPercent > 100)
        {
            throw new Exception("Discount percent cannot be greater than 100.");
        }

        switch (discountType)
        {
            case DiscountType.Expiry:
                if (!daysThreshold.HasValue)
                {
                    throw new Exception("Days threshold is required for expiry discounts.");
                }
                if (daysThreshold.Value <= 0)
                {
                    throw new Exception("Days threshold must be greater than zero.");
                }

                return new ExpiryDiscountRule(
                    GenerateDiscountRuleId(),
                    name,
                    isActive,
                    daysThreshold.Value,
                    discountPercent
                );

            case DiscountType.PurchaseTotal:
                if (!minimumAmount.HasValue)
                {
                    throw new Exception("Minimum amount is required for purchase total discounts.");
                }
                if (minimumAmount.Value <= 0)
                {
                    throw new Exception("Minimum purchase must be greater than zero.");
                }

                return new PurchaseTotalDiscountRule(
                    GenerateDiscountRuleId(),
                    name,
                    isActive,
                    minimumAmount.Value,
                    discountPercent
                );

            default:
                throw new Exception($"Unsupported discount type: {discountType}");
        }
    }

    public static string GenerateDiscountRuleId()
    {
        int highestNumber = 0;

        foreach (DiscountRule rule in supermarket.DiscountRules)
        {
            string numberText = rule.RuleId.Replace("D", "");

            bool isNumber = int.TryParse(numberText, out int number);

            if (isNumber)
            {
                if (number > highestNumber)
                {
                    highestNumber = number;
                }
            }
        }

        int newNumber = highestNumber + 1;

        return "D" + newNumber.ToString("D3");
    }
}

