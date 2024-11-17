using System.Runtime.InteropServices;

namespace BreweryAPI.Models
{
    public class Quote
    {
        public int Id { get; set; }
        public int WholesalerId { get; set; }
        public Wholesaler Wholesaler { get; set; }
        public DateTime RequestedAt { get; set; }
        public ICollection<QuoteDetail> Details { get; set; } = new List<QuoteDetail>();
        public decimal TotalPrice { get; set; }
        public string Summary { get; set; }
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }
    }
}
