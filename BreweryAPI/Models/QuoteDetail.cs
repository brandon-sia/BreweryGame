namespace BreweryAPI.Models
{
    public class QuoteDetail
    {
        public int Id { get; set; }
        public int QuoteId { get; set; }
        public Quote Quote { get; set; }
        public int BeerId { get; set; }
        public Beer Beer { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal LinePrice { get; set; }
    }
}
