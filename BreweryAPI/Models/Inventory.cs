namespace BreweryAPI.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public int WholesalerId { get; set; }
        public Wholesaler Wholesaler { get; set; }
        public int BeerId { get; set; }
        public Beer Beer { get; set; }
        public int Stock { get; set; } // Available stock for the beer at this wholesaler
        public decimal Price { get; set; } // Price per unit
    }
}
