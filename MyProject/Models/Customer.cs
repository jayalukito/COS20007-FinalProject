namespace MyProject.Models;

public class Customer: User
{
    public Cart Cart { get; set; } = new();
    public List<Receipt> Receipts { get; set; } = new();

    public Customer() { }
    public Customer(string userId, string name, string username, string password): base(userId, name, username, password)
    {
        Cart = new Cart();
        Receipts = new List<Receipt>();
    }

    public List<Receipt> ViewPurchaseHistory()
    {
        return Receipts;
    }

    public Receipt? ViewReceipt(string receiptId)
    {
        return Receipts.Find(receipt => receipt.ReceiptId == receiptId);
    }
}
