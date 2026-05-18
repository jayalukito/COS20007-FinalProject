namespace MyProject.Interface;
using MyProject.Models;

public interface IDiscountable
{
    decimal Apply(Cart cart, Inventory inventory);
}