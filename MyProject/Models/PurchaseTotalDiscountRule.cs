using MyProject.Interface;

namespace MyProject.Models;

public class PurchaseTotalDiscountRule : DiscountRule, IDiscountable, IDisplayable
{
    public decimal MinimumAmount { get; set; }
    public decimal DiscountPercent { get; set; }

    public PurchaseTotalDiscountRule()
    {
    }

    public PurchaseTotalDiscountRule(string ruleId, string name, bool isActive, decimal minimumAmount, decimal discountPercent)
        : base(ruleId, name, isActive)
    {
        MinimumAmount = minimumAmount;
        DiscountPercent = discountPercent;
    }

    public override decimal Apply(Cart cart, Inventory inventory)
    {
        if (!IsActive)
        {
            return 0;
        }

        if(cart.GetTotal() >= MinimumAmount)
        {
            return cart.GetTotal() * (DiscountPercent / 100);
        }
        else
        {
            return 0;
        }
    }

    public override string GetDisplayInformation()
    {
        string displayInformation = base.GetDisplayInformation();
        displayInformation += $"\nRule Type    : Purchase Total Discount\nMinimum Amount    : {MinimumAmount}\nDiscount Percent    : {DiscountPercent}%";
        return displayInformation;
    }
}
