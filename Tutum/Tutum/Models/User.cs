namespace Tutum.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string Phone { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public bool HasSubscription { get; set; }
    }
}
