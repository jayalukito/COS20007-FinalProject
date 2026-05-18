namespace MyProject.Controllers;

using MyProject.Models;

public class AuthController
{
    private readonly Supermarket supermarket;

    private const string CustomerFilePath = "Data/users.txt";

    private const string OwnerId = "O001";
    private const string OwnerName = "Owner";
    private const string OwnerUsername = "owner";
    private const string OwnerPassword = "admin123";

    private List<Customer> customers = new List<Customer>();
    public AuthController()
    {
        supermarket = Supermarket.Instance;
        LoadCustomersFromFile();
    }

    public Customer RegisterCustomer(string name, string username, string password)
    {
        ValidateRegisterInput(name, username, password);

        bool usernameAlreadyExists = false;

        foreach (Customer cust in customers)
        {
            if (cust.Username.Equals(username))
            {
                usernameAlreadyExists = true;
                break;
            }
        }

        if (usernameAlreadyExists)
        {
            throw new Exception("Username already exists.");
        }

        string customerId = GenerateCustomerId();

        Customer customer = new Customer(
            customerId,
            name,
            username,
            password
        );

        customers.Add(customer);

        SaveCustomersToFile();

        return customer;
    }

    public Customer LoginCustomer(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            throw new Exception("Username and password cannot be empty.");
        }

        Customer? customer = null;

        foreach (Customer cust in customers)
        {
            if (cust.Username.Equals(username) && cust.Password.Equals(password))
            {
                customer = cust;
                break;
            }
        }

        if (customer == null)
        {
            throw new Exception("Invalid username or password.");
        }

        supermarket.Customer = customer;
        return customer;
    }

    public Owner LoginOwner(string username, string password)
    {
        if (username != OwnerUsername || password != OwnerPassword)
        {
            throw new Exception("Invalid owner username or password.");
        }

        Owner owner = new Owner(
            OwnerId,
            OwnerName,
            OwnerUsername,
            OwnerPassword
        );

        Supermarket.Instance.Owner = owner;

        return owner;
    }

    private void LoadCustomersFromFile()
    {
        EnsureDataFileExists();

        string[] lines = File.ReadAllLines(CustomerFilePath);

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            string[] parts = line.Split('|');

            if (parts.Length != 4)
            {
                continue;
            }

            string userId = parts[0];
            string name = parts[1];
            string username = parts[2];
            string password = parts[3];

            bool alreadyLoaded = false;

            foreach (Customer customer in customers)
            {
                if (customer.UserId.Equals(userId))
                {
                    alreadyLoaded = true;
                    break;
                }
            }

            if (!alreadyLoaded)
            {
                Customer customer = new Customer(
                    userId,
                    name,
                    username,
                    password
                );

                customers.Add(customer);
            }
        }
    }

    private void SaveCustomersToFile()
    {
        EnsureDataFileExists();

        List<string> lines = new List<string>();

        foreach (Customer customer in customers)
        {
            string line = $"{customer.UserId}|{customer.Name}|{customer.Username}|{customer.Password}";
            lines.Add(line);
        }

        File.WriteAllLines(CustomerFilePath, lines);
    }

    private void EnsureDataFileExists()
    {
        string? directoryPath = Path.GetDirectoryName(CustomerFilePath);

        if (!string.IsNullOrWhiteSpace(directoryPath) && !Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        if (!File.Exists(CustomerFilePath))
        {
            File.Create(CustomerFilePath).Close();
        }
    }

    private string GenerateCustomerId()
    {
        int highestNumber = 0;

        foreach (Customer customer in customers)
        {
            string numericPart = customer.UserId.Replace("C", "");

            if (int.TryParse(numericPart, out int number))
            {
                if (number > highestNumber)
                {
                    highestNumber = number;
                }
            }
        }

        int newNumber = highestNumber + 1;

        return $"C{newNumber.ToString("D3")}";
    }

    private void ValidateRegisterInput(string name, string username, string password)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new Exception("Name cannot be empty.");
        }

        if (string.IsNullOrWhiteSpace(username))
        {
            throw new Exception("Username cannot be empty.");
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            throw new Exception("Password cannot be empty.");
        }

        if (password.Length < 3)
        {
            throw new Exception("Password must be at least 3 characters.");
        }
    }
}