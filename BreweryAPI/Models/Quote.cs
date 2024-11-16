namespace BreweryAPI.Models
{
    public class Quote
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<Beer> Beers { get; set; }
        public decimal DiscountApplied { get; set; }
    }
}
