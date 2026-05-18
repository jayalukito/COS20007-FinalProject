namespace MyProject.Models
{
    public class Owner : User
    {
        public Owner(
            string userId,
            string name,
            string username,
            string password
        ) : base(userId, name, username, password)
        {
        }
    }
}