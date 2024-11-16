namespace BreweryAPI.Models
{
    public class Wholesaler
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
    }
}
