using Store.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace Store.Data.Entities
{
    public class BookDetail : BaseEntity
    {
        [Required]
        public int BookId { get; set; }

        public virtual Book Book { get; set; }

        [Required]
        public int PageCount { get; set; }

        [Required]
        [StringLength(50)]
        public string Language { get; set; }

        [Required]
        [StringLength(100)]
        public string Publisher { get; set; }
    }
}
