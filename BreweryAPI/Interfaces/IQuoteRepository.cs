using BreweryAPI.Models;

namespace BreweryAPI.Interfaces
{
    public interface IQuoteRepository
    {
        ICollection<Quote> GetQuotes();
        Quote GetQuote(int id);
        bool QuoteExists(int id);
        bool CreateQuote(Quote inventory);
        bool UpdateQuote(Quote inventory);
        bool DeleteQuote(Quote inventory);
        bool Save();
    }
}
