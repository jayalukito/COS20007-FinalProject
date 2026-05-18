namespace MyProject.Models;

public class CartItem
{
    public Item Item { get; set; }
    public int Quantity { get; set; }

    public CartItem(Item item, int quantity)
    {
        Item = item;
        Quantity = quantity;
    }

    public decimal GetSubtotal()
    {
        return Item.SellPrice * Quantity;
    }

    public void UpdateQuantity(int quantity)
    {
        Quantity = quantity;
    }

    public void AddQuantity(int quantity)
    {
        Quantity += quantity;
    }
}
