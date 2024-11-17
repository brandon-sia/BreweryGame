using BreweryAPI.Models;

namespace BreweryAPI.Interfaces
{
    public interface IInventoryRepository
    {
        ICollection<Inventory> GetInventories();
        Inventory GetInventory(int id);
        bool InventoryExists(int id);
        bool CreateInventory(Inventory inventory);
        bool UpdateInventory(Inventory inventory);
        bool DeleteInventory(Inventory inventory);
        bool Save();
    }
}
