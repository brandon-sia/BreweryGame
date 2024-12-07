namespace BreweryAPI.Models
{
    public class Beer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrewerId { get; set; }
        public Brewery Brewer { get; set; }
        public ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
        
    }
}
