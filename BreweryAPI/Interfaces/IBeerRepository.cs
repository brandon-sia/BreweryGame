using BreweryAPI.Models;

namespace BreweryAPI.Interfaces
{
    public interface IBeerRepository
    {
        ICollection<Beer> GetBeers();
        Beer GetBeer(int id);
        ICollection<Brewery> GetBreweryByBeer(int beerId);
        bool BeerExists(int id);
        bool CreateBeer(Beer beer);
        bool UpdateBeer(Beer beer);
        bool DeleteBeer(Beer beer);
        bool Save();
    }
}
