namespace Tutum.Models
{
    public partial class User
    {
        public int UserId { get; set; }

        public string Phone { get; set; }

        public bool HasSubscription { get; set; }
    }
}
