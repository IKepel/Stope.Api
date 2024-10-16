using System.ComponentModel.DataAnnotations;

namespace Store.Data.Common
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
