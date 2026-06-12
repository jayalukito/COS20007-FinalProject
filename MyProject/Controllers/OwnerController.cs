using MyProject.Models;

namespace MyProject.Controllers;

public class OwnerController
{
    public OwnerController()
    {
    }

    public OwnerController(Owner owner)
    {
    }

    public void Logout()
    {
        Supermarket instance = Supermarket.Instance;
        instance.Owner = null;
    }
}
