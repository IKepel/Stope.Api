using System.ComponentModel.DataAnnotations;

namespace Store.Business.Models.BookDetails
{
    public class BookDetailModel
    {
        public string Language { get; set; }

        public int PageCount { get; set; }

        public string Publisher { get; set; }
    }
}
