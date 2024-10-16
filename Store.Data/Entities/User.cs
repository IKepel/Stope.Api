using Store.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace Store.Data.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(250)]
        public string Email { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
