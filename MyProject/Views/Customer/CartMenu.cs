namespace MyProject.Views;

using MyProject.Controllers;
using MyProject.Models;
using MyProject.Utils;

public class CartMenu : BaseMenu
{
    private InventoryController inventoryController = new();
    private CartController cartController = new();

    public override void Show()
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.Clear();

            Console.WriteLine("=== Cart Menu ===");
            Console.WriteLine("1. View Cart");
            Console.WriteLine("2. Update Item Quantity");
            Console.WriteLine("3. Remove Item from Cart");
            Console.WriteLine("4. Clear Cart");
            Console.WriteLine("5. Go Back");
            Console.Write("Enter your choice: ");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    ShowCart();
                    break;

                case "2":
                    ShowUpdateQuantity();
                    break;

                case "3":
                    ShowRemoveItem();
                    break;

                case "4":
                    ShowClearCart();
                    break;

                case "5":
                    isRunning = false;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    Pause();
                    break;
            }
        }
    }

    private void ShowCart()
    {
        Console.Clear();
        Console.WriteLine("=== Your Cart ===");

        try
        {
            Cart cart = cartController.GetCart();

            if (cart.Items.Count == 0)
            {
                Console.WriteLine("Your cart is empty.");
                Pause();
                return;
            }

            DisplayCartItems(cart);

            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"Total: {cart.GetTotal():C}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Pause();
    }

    private void ShowUpdateQuantity()
    {
        Console.Clear();
        Console.WriteLine("=== Update Item Quantity ===");

        try
        {
            Cart cart = cartController.GetCart();

            if (cart.Items.Count == 0)
            {
                Console.WriteLine("Your cart is empty.");
                Pause();
                return;
            }

            DisplayCartItems(cart);

            string itemId = InputHelper.ReadRequiredString("Enter Item ID: ");
            int quantity = InputHelper.ReadRequiredInt("Enter new quantity: ");

            

            cartController.UpdateQuantity(inventoryController.GetItemById(itemId), quantity);

            Console.WriteLine("Cart item quantity updated successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Pause();
    }

    private void ShowRemoveItem()
    {
        Console.Clear();
        Console.WriteLine("=== Remove Item from Cart ===");

        try
        {
            Cart cart = cartController.GetCart();

            if (cart.Items.Count == 0)
            {
                Console.WriteLine("Your cart is empty.");
                Pause();
                return;
            }

            DisplayCartItems(cart);

            string itemId = InputHelper.ReadRequiredString("Enter Item ID to remove: ");

            cartController.RemoveItemFromCart(itemId);

            Console.WriteLine("Item removed from cart successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Pause();
    }

    private void ShowClearCart()
    {
        Console.Clear();
        Console.WriteLine("=== Clear Cart ===");

        try
        {
            Cart cart = cartController.GetCart();

            if (cart.Items.Count == 0)
            {
                Console.WriteLine("Your cart is already empty.");
                Pause();
                return;
            }

            DisplayCartItems(cart);

            string confirmation = InputHelper.ReadRequiredString("Are you sure you want to clear cart? (Y/N): ");

            if (confirmation.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                cartController.ClearCart();
                Console.WriteLine("Cart cleared successfully.");
            }
            else
            {
                Console.WriteLine("Clear cart cancelled.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Pause();
    }


    private void DisplayCartItems(Cart cart)
    {
        foreach (CartItem cartItem in cart.Items)
        {
            Console.WriteLine($"Item ID   : {cartItem.Item.ItemId}");
            Console.WriteLine($"Name      : {cartItem.Item.Name}");
            Console.WriteLine($"Price     : {cartItem.Item.SellPrice:C}");
            Console.WriteLine($"Quantity  : {cartItem.Quantity}");
            Console.WriteLine($"Subtotal  : {cartItem.GetSubtotal():C}");
            Console.WriteLine("----------------------------------------");
        }
    }
}