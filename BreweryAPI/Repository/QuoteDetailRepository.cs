using BreweryAPI.Data;
using BreweryAPI.Interfaces;
using BreweryAPI.Models;

namespace BreweryAPI.Repository
{
    public class QuoteDetailRepository : IQuoteDetailRepository
    {
        private DataContext _context;
        public QuoteDetailRepository(DataContext context)
        {
            _context = context;
        }
        public bool QuoteDetailExists(int id)
        {
            return _context.QuoteDetails.Any(b => b.Id == id);
        }

        public bool CreateQuoteDetail(QuoteDetail QuoteDetail)
        {
            // Change Tracker
            _context.Add(QuoteDetail);
            return Save();
        }

        public bool DeleteQuoteDetail(QuoteDetail QuoteDetail)
        {
            _context.Remove(QuoteDetail);
            return Save();
        }

        public QuoteDetail GetQuoteDetail(int id)
        {
            return _context.QuoteDetails.Where(e => e.Id == id).FirstOrDefault();
        }

        public ICollection<QuoteDetail> GetQuoteDetails()
        {
            return _context.QuoteDetails.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateQuoteDetail(QuoteDetail QuoteDetail)
        {
            _context.Update(QuoteDetail);
            return Save();
        }
    }
}
