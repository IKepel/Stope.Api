using Store.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace Store.Data.Entities
{
    public class Author : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(1000)]
        public string Biography { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
