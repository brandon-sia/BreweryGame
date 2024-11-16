using BreweryAPI.Data;
using BreweryAPI.Interfaces;
using BreweryAPI.Models;

namespace BreweryAPI.Repository
{
    public class WholeSalerRepository : IWholesalerRepository
    {
        private DataContext _context;
        public WholeSalerRepository(DataContext context)
        {
            _context = context;
        }
        public bool WholesalerExists(int id)
        {
            return _context.Wholesalers.Any(b => b.Id == id);
        }

        public bool CreateWholesaler(Wholesaler wholesaler)
        {
            // Change Tracker
            _context.Add(wholesaler);
            return Save();
        }

        public bool DeleteWholesaler(Wholesaler wholesaler)
        {
            _context.Remove(wholesaler);
            return Save();
        }

        public Wholesaler GetWholesaler(int id)
        {
            return _context.Wholesalers.Where(e => e.Id == id).FirstOrDefault();
        }

        public ICollection<Wholesaler> GetWholesalers()
        {
            return _context.Wholesalers.ToList();
        }

        public ICollection<Beer> GetBeerByWholesaler(int wholesalerId)
        {
            return _context.Beers.Where(w => w.Id == wholesalerId).Select(b => b).ToList();
        }

        public ICollection<Brewery> GetBreweryByWholesaler(int wholesalerId)
        {
            return _context.Breweries.Where(w => w.Id == wholesalerId).Select(b => b).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateWholesaler(Wholesaler wholesaler)
        {
            _context.Update(wholesaler);
            return Save();
        }
    }
}
