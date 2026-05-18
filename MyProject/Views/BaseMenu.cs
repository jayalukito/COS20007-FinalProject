namespace MyProject.Views;
public abstract class BaseMenu
{
    public BaseMenu()
    {}

    protected void Pause()
    {
        
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }

    public virtual void Show(){}
}