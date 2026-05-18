namespace MyProject.Models;

public class NonPerishableItem : Item
{
    public NonPerishableItem() { }
    public NonPerishableItem(string itemId,string name, decimal sellPrice, decimal costPrice, int quantity, Category category) : base(itemId, name, sellPrice, costPrice, quantity, category)
    {
        
    }
}