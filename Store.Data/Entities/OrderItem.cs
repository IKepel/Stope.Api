using Store.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Data.Entities
{
    public class OrderItem : BaseEntity
    {
        [Required]
        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        [Required]
        public int BookId { get; set; }

        public virtual Book Book { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
