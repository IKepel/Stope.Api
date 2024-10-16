using Store.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Data.Entities
{
    public class Book : BaseEntity
    {
        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [Required]
        public DateTime PublishedDate { get; set; }

        public virtual ICollection<Author> Authors { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<BookDetail> BookDetails { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
