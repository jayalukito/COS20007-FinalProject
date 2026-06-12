namespace MyProject.Models;
public sealed class Supermarket
{
    private static Supermarket? _instance;
    public Inventory Inventory { get; private set;} = new Inventory();
    public List<Category> Categories { get; set; } = new();
    public Customer? Customer;
    public Owner? Owner;
    public List<Receipt> Receipts { get; private set; } = new List<Receipt>();

    public List<SalesRecord> SalesRecords { get; private set; } = new List<SalesRecord>();
    public List<DiscountRule> DiscountRules { get; private set; } = new List<DiscountRule>();   
    private Supermarket()
    {
        Inventory = new Inventory();
    }

    public static Supermarket Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Supermarket();
                SupermarketSeeder();
            }
            return _instance;
        }
    }

    private static void SupermarketSeeder()
    {
        if (_instance == null)
        {
            return;
        }

        if (_instance.Categories.Count == 0)
        {
            Category food = new Category("CAT001", "Food");
            Category drink = new Category("CAT002", "Drink");
            Category electronics = new Category("CAT003", "Electronics");
            Category household = new Category("CAT004", "Household");

            _instance.Categories.Add(food);
            _instance.Categories.Add(drink);
            _instance.Categories.Add(electronics);
            _instance.Categories.Add(household);
        }

        if (_instance.Inventory.Items.Count == 0)
        {
            Category? food = _instance.Categories.FirstOrDefault(category => category.CategoryId == "CAT001");
            Category? drink = _instance.Categories.FirstOrDefault(category => category.CategoryId == "CAT002");
            Category? electronics = _instance.Categories.FirstOrDefault(category => category.CategoryId == "CAT003");
            Category? household = _instance.Categories.FirstOrDefault(category => category.CategoryId == "CAT004");

            if (food == null || drink == null || electronics == null || household == null)
            {
                return;
            }

            PerishableItem milk = new PerishableItem(
                string.Empty,
                "Fresh Milk",
                15000,
                10000,
                20,
                drink,
                DateTime.Today.AddDays(5)
            );

            PerishableItem bread = new PerishableItem(
                string.Empty,
                "Bread",
                12000,
                7000,
                15,
                food,
                DateTime.Today.AddDays(3)
            );

            PerishableItem chicken = new PerishableItem(
                string.Empty,
                "Chicken Meat",
                45000,
                32000,
                10,
                food,
                DateTime.Today.AddDays(2)
            );

            ElectronicsItem laptop = new ElectronicsItem(
                string.Empty,
                "Laptop",
                7500000,
                6000000,
                5,
                electronics,
                24,
                "Lenovo"
            );

            ElectronicsItem mouse = new ElectronicsItem(
                string.Empty,
                "Wireless Mouse",
                150000,
                90000,
                30,
                electronics,
                12,
                "Logitech"
            );

            ElectronicsItem riceCooker = new ElectronicsItem(
                string.Empty,
                "Rice Cooker",
                450000,
                320000,
                8,
                household,
                12,
                "Philips"

                
            );

            _instance.Inventory.AddItem(milk);
            _instance.Inventory.AddItem(bread);
            _instance.Inventory.AddItem(chicken);
            _instance.Inventory.AddItem(laptop);
            _instance.Inventory.AddItem(mouse);
            _instance.Inventory.AddItem(riceCooker);
        }

        if (_instance.DiscountRules.Count == 0)
        {
            ExpiryDiscountRule expiryDiscountRule = new ExpiryDiscountRule(
                "D001",
                "Near Expiry Discount",
                true,
                7,
                20
            );

            PurchaseTotalDiscountRule purchaseTotalDiscountRule = new PurchaseTotalDiscountRule(
                "D002",
                "Large Purchase Discount",
                true,
                100000,
                10
            );

            _instance.DiscountRules.Add(expiryDiscountRule);
            _instance.DiscountRules.Add(purchaseTotalDiscountRule);
        }
    }
}