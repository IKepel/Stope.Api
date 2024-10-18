using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stope.Api.Models.Requests
{
    public class OrderRequestModel
    {
        public int Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public List<OrderItemRequestModel> OrderItems { get; set; }
    }
}
