using BreweryAPI.Models;

namespace BreweryAPI.DTO
{
    public class QuoteDTO
    {
        public int Id { get; set; }
        public int WholesalerId { get; set; }
        public DateTime RequestedAt { get; set; }
        public decimal TotalPrice { get; set; }
        public string Summary { get; set; }
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }
    }
}
