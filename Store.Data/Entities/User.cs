﻿namespace Store.Data.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
