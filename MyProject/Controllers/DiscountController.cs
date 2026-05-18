namespace MyProject.Controllers;

using MyProject.Models;

public class DiscountController
{
    private readonly Supermarket supermarket;

    public DiscountController()
    {
        supermarket = Supermarket.Instance;
    }

    public List<DiscountRule> GetAllDiscountRules()
    {
        return supermarket.DiscountRules;
    }

    public DiscountRule? GetDiscountRuleById(string ruleId)
    {
        if (string.IsNullOrWhiteSpace(ruleId))
        {
            return null;
        }

        foreach (DiscountRule rule in supermarket.DiscountRules)
        {
            if (rule.RuleId.Equals(ruleId.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                return rule;
            }
        }

        return null;
    }

    public void AddDiscountRule(DiscountRule discountRule)
    {
        if (discountRule == null)
        {
            throw new Exception("Discount rule cannot be null.");
        }

        if (string.IsNullOrWhiteSpace(discountRule.RuleId))
        {
            discountRule.RuleId = GenerateDiscountRuleId();
        }

        if (string.IsNullOrWhiteSpace(discountRule.Name))
        {
            throw new Exception("Discount rule name cannot be empty.");
        }

        bool ruleExists = false;

        foreach (DiscountRule rule in supermarket.DiscountRules)
        {
            if (rule.RuleId.Equals(discountRule.RuleId, StringComparison.OrdinalIgnoreCase))
            {
                ruleExists = true;
                break;
            }
        }

        if (ruleExists)
        {
            throw new Exception("Discount rule ID already exists.");
        }

        ValidateDiscountRule(discountRule);

        supermarket.DiscountRules.Add(discountRule);
    }

    public void RemoveDiscountRule(string ruleId)
    {
        DiscountRule? discountRule = GetDiscountRuleById(ruleId);

        if (discountRule == null)
        {
            throw new Exception("Discount rule not found.");
        }

        supermarket.DiscountRules.Remove(discountRule);
    }

    public void ActivateDiscountRule(string ruleId)
    {
        DiscountRule? discountRule = GetDiscountRuleById(ruleId);

        if (discountRule == null)
        {
            throw new Exception("Discount rule not found.");
        }

        discountRule.Activate();
    }

    public void DeactivateDiscountRule(string ruleId)
    {
        DiscountRule? discountRule = GetDiscountRuleById(ruleId);

        if (discountRule == null)
        {
            throw new Exception("Discount rule not found.");
        }

        discountRule.Deactivate();
    }

    public decimal CalculateDiscount(Cart cart)
    {
        if (cart == null)
        {
            throw new Exception("Cart cannot be null.");
        }

        decimal totalDiscount = 0;

        foreach (DiscountRule rule in supermarket.DiscountRules)
        {
            if (rule.IsActive)
            {
                totalDiscount += rule.Apply(cart, supermarket.Inventory);
            }
        }

        decimal cartTotal = cart.GetTotal();

        if (totalDiscount > cartTotal)
        {
            totalDiscount = cartTotal;
        }

        return totalDiscount;
    }

    public string GenerateDiscountRuleId()
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

    private void ValidateDiscountRule(DiscountRule discountRule)
    {
        if (discountRule is ExpiryDiscountRule expiryRule)
        {
            if (expiryRule.DaysThreshold <= 0)
            {
                throw new Exception("Days threshold must be greater than zero.");
            }

            if (expiryRule.DiscountPercent <= 0)
            {
                throw new Exception("Discount percent must be greater than zero.");
            }

            if (expiryRule.DiscountPercent > 100)
            {
                throw new Exception("Discount percent cannot be greater than 100.");
            }
        }
        else if (discountRule is PurchaseTotalDiscountRule purchaseRule)
        {
            if (purchaseRule.MinimumAmount <= 0)
            {
                throw new Exception("Minimum purchase must be greater than zero.");
            }

            if (purchaseRule.DiscountPercent <= 0)
            {
                throw new Exception("Discount percent must be greater than zero.");
            }

            if (purchaseRule.DiscountPercent > 100)
            {
                throw new Exception("Discount percent cannot be greater than 100.");
            }
        }
    }
}