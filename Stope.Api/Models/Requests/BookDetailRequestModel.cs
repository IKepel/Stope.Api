using System.ComponentModel.DataAnnotations;

namespace Stope.Api.Models.Requests
{
    public class BookDetailRequestModel
    {
        [Required]
        public string Language { get; set; }

        [Required]
        public int PageCount { get; set; }

        [Required]
        public string Publisher { get; set; }
    }
}
