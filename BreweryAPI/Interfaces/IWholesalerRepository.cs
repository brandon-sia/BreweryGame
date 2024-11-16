using BreweryAPI.Models;

namespace BreweryAPI.Interfaces
{
    public interface IWholesalerRepository
    {
        ICollection<Wholesaler> GetWholesalers();
        Wholesaler GetWholesaler(int id);
        ICollection<Beer> GetBeerByWholesaler(int wholesalerId);
        ICollection<Brewery> GetBreweryByWholesaler(int wholesalerId);
        bool WholesalerExists(int id);
        bool CreateWholesaler(Wholesaler wholesaler);
        bool UpdateWholesaler(Wholesaler wholesaler);
        bool DeleteWholesaler(Wholesaler wholesaler);
        bool Save();
    }
}
