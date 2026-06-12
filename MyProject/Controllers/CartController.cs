namespace MyProject.Controllers;

using MyProject.Controllers;
using MyProject.Models;
using MyProject.Utils;

public class CartController
{
    Supermarket supermarket = Supermarket.Instance;
    public CartController()
    {
        supermarket = Supermarket.Instance;
    }

    public Cart GetCart()
    {
        return supermarket.Customer.Cart;
    }

    public void AddItemToCart(Item item, int quantity)
    {
        CartItem cartItem = new CartItem(item, quantity);
        Cart cart = supermarket.Customer.Cart;

        if(supermarket.Inventory.StockChecker(item, quantity) == false)
        {
            throw new Exception("Item is out of stock.");
        }
      
        bool itemExist = false;
        foreach(CartItem existingItem in cart.Items)
        {
            if(existingItem.Item.ItemId.Equals(item.ItemId, StringComparison.OrdinalIgnoreCase))
            {
                existingItem.AddQuantity(quantity);
                itemExist = true;
                return;
            }
        }

        if (!itemExist)
        {
            cart.AddItem(cartItem);
        }

        supermarket.Customer.Cart = cart;
    }

    public void RemoveItemFromCart(string itemId)
    {
        foreach(CartItem cartItem in supermarket.Customer.Cart.Items)
        {
            if(cartItem.Item.ItemId.Equals(itemId, StringComparison.OrdinalIgnoreCase))
            {
                supermarket.Customer.Cart.RemoveItem(itemId);
                return;
            }
        }
        
        throw new Exception("Item not found in cart.");
    }

    public void UpdateQuantity(Item item, int quantity)
    {

        if(!supermarket.Inventory.StockChecker(item, quantity))
        {
            throw new Exception("Item is out of stock.");
        };

        foreach(CartItem cartItem in supermarket.Customer.Cart.Items)
        {
            if(cartItem.Item.ItemId.Equals(item.ItemId, StringComparison.OrdinalIgnoreCase))
            {
                cartItem.UpdateQuantity(quantity);
                return;
            }

        }

        throw new Exception("Item not found in cart.");
    }

    public void ClearCart()
    {
        supermarket.Customer.Cart.Clear();
    }

   
    
}