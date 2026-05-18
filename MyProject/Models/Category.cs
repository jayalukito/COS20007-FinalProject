namespace MyProject.Models
{
    public class Category
    {
        public string CategoryId { get; set; }
        public string Name { get; set; }

        public Category(){}

        public Category (string categoryId, string name)
        {
            CategoryId = categoryId;
            Name = name;
        }
        public string GetDisplayInformation()
        {
            string displayInformation = $"Category ID: {CategoryId}\nName: {Name}\n";
            return displayInformation;
        }
    }
}