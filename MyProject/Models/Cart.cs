namespace MyProject.Models;

public class Cart
{
    public string CustomerId { get; set; } = string.Empty;
    public List<CartItem> Items { get; set; } = new();

    public Cart()
    {
    }

    public Cart(string customerId)
    {
        CustomerId = customerId;
    }

    public void AddItem(CartItem item)
    {
        Items.Add(item);
    }

    public void RemoveItem(string itemId)
    {
        Items.RemoveAll(item => item.Item.ItemId == itemId);
    }

    public decimal GetTotal()
    {
        return Items.Sum(item => item.GetSubtotal());
    }

    public void Clear()
    {
        Items.Clear();
    }

    public bool IsEmpty()
    {
        return Items.Count == 0;
    }

    public void UpdateQuantity(string itemId, int quantity)
    {
        var item = Items.Find(item => item.Item.ItemId == itemId);
        item?.UpdateQuantity(quantity);
    }
}
