using BreweryAPI.Data;
using BreweryAPI.Interfaces;
using BreweryAPI.Models;

namespace BreweryAPI.Repository
{
    public class InventoryRepository : IInventoryRepository
    {
        private DataContext _context;
        public InventoryRepository(DataContext context)
        {
            _context = context;
        }
        public bool InventoryExists(int id)
        {
            return _context.Inventories.Any(b => b.Id == id);
        }

        public bool CreateInventory(Inventory inventory)
        {
            // Change Tracker
            _context.Add(inventory);
            return Save();
        }

        public bool DeleteInventory(Inventory inventory)
        {
            _context.Remove(inventory);
            return Save();
        }

        public Inventory GetInventory(int id)
        {
            return _context.Inventories.Where(e => e.Id == id).FirstOrDefault();
        }

        public ICollection<Inventory> GetInventories()
        {
            return _context.Inventories.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateInventory(Inventory inventory)
        {
            _context.Update(inventory);
            return Save();
        }
    }
}
