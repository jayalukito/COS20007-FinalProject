using MyProject.Interface;

namespace MyProject.Models;

public class ExpiryDiscountRule : DiscountRule, IDiscountable, IDisplayable
{
    public int DaysThreshold { get; set; }
    public decimal DiscountPercent { get; set; }

    public ExpiryDiscountRule()
    {
    }

    public ExpiryDiscountRule(string ruleId, string name, bool isActive, int daysThreshold, decimal discountPercent)
        : base(ruleId, name, isActive)
    {
        DaysThreshold = daysThreshold;
        DiscountPercent = discountPercent;
    }

    public override decimal Apply(Cart cart, Inventory inventory)
    {
        if (!IsActive)
        {
            return 0;
        }

        decimal discountAmount = 0;

        foreach (CartItem cartItem in cart.Items)
        {
            if (cartItem.Item is IExpirable expirableItem)
            {
                int daysLeft = expirableItem.DaysUntilExpiry();

                if (daysLeft >= 0 && daysLeft <= DaysThreshold)
                {
                    discountAmount += cartItem.GetSubtotal() * (DiscountPercent / 100);
                }
            }
        }

        return discountAmount;
    }
    
    public override string GetDisplayInformation()
    {
        string displayInformation = base.GetDisplayInformation();
        displayInformation += $"\nRule Type    : Expiry Discount\nDays Threshold    : {DaysThreshold}\nDiscount Percent    : {DiscountPercent}%";
        return displayInformation;
    }
}
