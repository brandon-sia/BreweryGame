using BreweryAPI.Data;
using BreweryAPI.Interfaces;
using BreweryAPI.Models;
using System.Diagnostics.Metrics;

namespace BreweryAPI.Repository
{
    public class BeerRepository : IBeerRepository
    {
        private DataContext _context;
        public BeerRepository(DataContext context)
        {
            _context = context;
        }
        public bool BeerExists(int id)
        {
            return _context.Beers.Any(b => b.Id == id);
        }

        public bool CreateBeer(Beer beer)
        {
            // Change Tracker
            _context.Add(beer);
            return Save();
        }

        public bool DeleteBeer(Beer beer)
        {
            _context.Remove(beer);
            return Save();
        }

        public Beer GetBeer(int id)
        {
            return _context.Beers.Where(e => e.Id == id).FirstOrDefault();
        }

        public ICollection<Beer> GetBeers()
        {
            return _context.Beers.ToList();
        }

        public ICollection<Brewery> GetBreweryByBeer(int beerId)
        {
            return _context.Breweries.Where(b => b.Id == beerId).Select(br=>br).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateBeer(Beer beer)
        {
            _context.Update(beer);
            return Save();
        }
    }
}
