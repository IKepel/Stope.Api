﻿namespace Store.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }
    }
}