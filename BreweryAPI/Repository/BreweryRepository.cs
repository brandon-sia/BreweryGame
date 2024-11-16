using BreweryAPI.Data;
using BreweryAPI.Interfaces;
using BreweryAPI.Models;

namespace BreweryAPI.Repository
{
    public class BreweryRepository : IBreweryRepository
    {
        private DataContext _context;
        public BreweryRepository(DataContext context)
        {
            _context = context;
        }
        public bool BreweryExists(int id)
        {
            return _context.Breweries.Any(b => b.Id == id);
        }

        public bool CreateBrewery(Brewery brewery)
        {
            // Change Tracker
            _context.Add(brewery);
            return Save();
        }

        public bool DeleteBrewery(Brewery brewery)
        {
            _context.Remove(brewery);
            return Save();
        }

        public Brewery GetBrewery(int id)
        {
            return _context.Breweries.Where(e => e.Id == id).FirstOrDefault();
        }

        public ICollection<Brewery> GetBreweries()
        {
            return _context.Breweries.ToList();
        }

        public ICollection<Beer> GetBeerByBrewery(int breweryId)
        {
            return _context.Beers.Where(br => br.Id == breweryId).Select(b=>b).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateBrewery(Brewery brewery)
        {
            _context.Update(brewery);
            return Save();
        }
    }
}
