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

    

    
}