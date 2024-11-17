using BreweryAPI.Models;

namespace BreweryAPI.DTO
{
    public class InventoryDTO
    {
        public int Id { get; set; }
        public int WholesalerId { get; set; }
        public int BeerId { get; set; }
        public int Stock { get; set; } // Available stock for the beer at this wholesaler
        public decimal Price { get; set; } // Price per unit
    }
}
