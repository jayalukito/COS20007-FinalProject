namespace MyProject.Models
{
    public abstract class User
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        protected User() { }
        protected User(string userId, string name, string username, string password)
        {
            UserId = userId;
            Name = name;
            Username = username;
            Password = password;
        }
    }
}