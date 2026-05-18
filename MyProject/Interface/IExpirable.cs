namespace MyProject.Models
{
    public interface IExpirable
    {
        DateTime ExpiryDate { get; set; }

        bool IsExpired();

        int DaysUntilExpiry();
    }
}