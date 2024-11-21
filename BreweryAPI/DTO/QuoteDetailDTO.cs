using BreweryAPI.Models;

namespace BreweryAPI.DTO
{
    public class QuoteDetailDTO
    {
        public int Id { get; set; }
        public int QuoteId { get; set; }
        public int BeerId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal LinePrice { get; set; }
    }
}
