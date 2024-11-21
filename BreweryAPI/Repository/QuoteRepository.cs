using BreweryAPI.Data;
using BreweryAPI.Interfaces;
using BreweryAPI.Models;

namespace BreweryAPI.Repository
{
    public class QuoteRepository : IQuoteRepository
    {
        private DataContext _context;
        public QuoteRepository(DataContext context)
        {
            _context = context;
        }
        public bool QuoteExists(int id)
        {
            return _context.Quotes.Any(b => b.Id == id);
        }

        public bool CreateQuote(Quote Quote)
        {
            // Change Tracker
            _context.Add(Quote);
            return Save();
        }

        public bool DeleteQuote(Quote Quote)
        {
            _context.Remove(Quote);
            return Save();
        }

        public Quote GetQuote(int id)
        {
            return _context.Quotes.Where(e => e.Id == id).FirstOrDefault();
        }

        public ICollection<Quote> GetQuotes()
        {
            return _context.Quotes.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateQuote(Quote Quote)
        {
            _context.Update(Quote);
            return Save();
        }
    }
}
