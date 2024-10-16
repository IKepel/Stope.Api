using Store.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace Store.Data.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
