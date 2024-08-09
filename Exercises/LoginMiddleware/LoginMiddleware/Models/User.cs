namespace LoginMiddleware.Models
{
    public class User
    {

        public User()
        {
            email = "admin@example.com";
            password = "admin1234";
        }

        public string email { get; set; }
        public string password { get; set; }
    }
}
