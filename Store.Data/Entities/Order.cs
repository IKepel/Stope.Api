﻿using Store.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Data.Entities
{
    public class Order : BaseEntity
    {
        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
