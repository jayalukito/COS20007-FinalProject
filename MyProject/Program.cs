namespace MyProject;

using MyProject.Controllers;
using MyProject.Views;

public class Program
{
    public static void Main(string[] args)
    {
        AuthController authController = new AuthController();
        LoginMenu loginMenu = new LoginMenu(authController);

        loginMenu.Show();
    }
}