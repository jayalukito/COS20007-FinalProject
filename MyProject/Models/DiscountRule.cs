namespace MyProject.Models;

using System.Data;

using MyProject.Interface;
public abstract class DiscountRule: IDiscountable, IDisplayable
{
    public string RuleId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }

    protected DiscountRule()
    {
    }

    protected DiscountRule(string ruleId, string name, bool isActive)
    {
        RuleId = ruleId;
        Name = name;
        IsActive = isActive;
    }

    public abstract decimal Apply(Cart cart, Inventory inventory);

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public virtual string GetDisplayInformation()
    {
        string displayInformation = $"Rule ID    : {RuleId}\nName    : {Name}\nStatus    :{(IsActive ? "Active" : "Inactive")}";
        return displayInformation;
    }
}
