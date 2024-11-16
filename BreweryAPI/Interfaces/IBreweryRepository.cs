using BreweryAPI.Models;

namespace BreweryAPI.Interfaces
{
    public interface IBreweryRepository
    {
        ICollection<Brewery> GetBreweries();
        Brewery GetBrewery(int id);
        ICollection<Beer> GetBeerByBrewery(int breweryId);
        bool BreweryExists(int id);
        bool CreateBrewery(Brewery brewery);
        bool UpdateBrewery(Brewery brewery);
        bool DeleteBrewery(Brewery brewery);
        bool Save();
    }
}
