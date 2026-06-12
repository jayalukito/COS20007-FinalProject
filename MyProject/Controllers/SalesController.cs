namespace MyProject.Controllers;

using System.Security.Cryptography;
using MyProject.Controllers;
using MyProject.Models;
using MyProject.Utils;

public class SalesController 
{
    private Supermarket supermarket = Supermarket.Instance;

    public SalesController()
    {
        supermarket = Supermarket.Instance;
    }

    public List<SalesRecord> GetSalesRecords()
    {
        return supermarket.SalesRecords;
    }

    public Receipt Checkout()
    {
        if (supermarket.Customer == null)
        {
            throw new Exception("No customer is currently logged in.");
        }

        Customer customer = supermarket.Customer;
        Cart cart = customer.Cart;

        if (cart.Items.Count == 0)
        {
            throw new Exception("Cart is empty.");
        }

        ValidateCartStock(cart);

        decimal subtotal = cart.GetTotal();

        DiscountController discountController = new DiscountController();
        decimal discountAmount = discountController.CalculateDiscount(cart);
        decimal finalTotal = subtotal - discountAmount;

        if (finalTotal < 0)
        {
            finalTotal = 0;
        }

        List<CartItem> purchasedItems = CreatePurchasedItemsSnapshot(cart);

        ReduceInventoryStock(cart);

        Receipt receipt = new Receipt(
            GenerateReceiptId(),
            customer.UserId,
            DateTime.Now,
            finalTotal,
            discountAmount,
            purchasedItems
        );

        customer.Receipts.Add(receipt);
        supermarket.Receipts.Add(receipt);

        SalesRecord salesRecord = new SalesRecord(
            GenerateSalesRecordId(),
            receipt.CustomerId,
            receipt.Timestamp,
            receipt.Total,
            receipt.DiscountAmount,
            receipt.PurchasedItems
        );

        supermarket.SalesRecords.Add(salesRecord);

        cart.Clear();

        return receipt;
    }

    private void ValidateCartStock(Cart cart)
    {
        foreach (CartItem cartItem in cart.Items)
        {
            if (cartItem.Quantity <= 0)
            {
                throw new Exception("Cart item quantity must be greater than zero.");
            }

            if (cartItem.Quantity > cartItem.Item.StockQuantity)
            {
                throw new Exception(
                    $"Not enough stock for {cartItem.Item.Name}. Available stock: {cartItem.Item.StockQuantity}"
                );
            }
        }
    }

    private void ReduceInventoryStock(Cart cart)
    {
        foreach (CartItem cartItem in cart.Items)
        {
            cartItem.Item.ReduceStock(cartItem.Quantity);
        }
    }

    private List<CartItem> CreatePurchasedItemsSnapshot(Cart cart)
    {
        List<CartItem> purchasedItems = new List<CartItem>();

        foreach (CartItem cartItem in cart.Items)
        {
            CartItem purchasedItem = new CartItem(
                cartItem.Item,
                cartItem.Quantity
            );

            purchasedItems.Add(purchasedItem);
        }

        return purchasedItems;
    }

    private string GenerateReceiptId()
    {
        int highestNumber = 0;

        foreach (Receipt receipt in supermarket.Receipts)
        {
            string numberText = receipt.ReceiptId.Replace("R", "");

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

        return "R" + newNumber.ToString("D3");
    }
    
    private string GenerateSalesRecordId()
    {
        int highestNumber = 0;

        foreach (SalesRecord salesRecord in supermarket.SalesRecords)
        {
            string numberText = salesRecord.RecordId.Replace("SR", "");

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

        return "SR" + newNumber.ToString("D3");
    }

    public SalesReport GenerateReportByCategory(string categoryId)
    {

        SalesReport salesReport = new SalesReport(supermarket.SalesRecords);
        try
        {
            return salesReport.FilterByCategory(categoryId);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public SalesReport GenerateReportByMonth(int month, int year)
    {
        SalesReport salesReport = new SalesReport(supermarket.SalesRecords);
        try
        {
            return salesReport.FilterByMonth(month, year);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}