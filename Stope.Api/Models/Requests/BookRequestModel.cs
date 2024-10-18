using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stope.Api.Models.Requests
{
    public class BookRequestModel
    {
        public int Id { get; set; }

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

        [Required]
        public List<int> AuthorIds { get; set; }

        [Required]
        public List<int> CategoryIds { get; set; }

        [Required]
        public List<BookDetailRequestModel> BookDetails { get; set; }
    }
}
