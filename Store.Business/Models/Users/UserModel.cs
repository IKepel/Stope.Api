using Store.Data.Entities;

namespace Store.Business.Models.Users
{
    public class UserModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
