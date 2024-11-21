using BreweryAPI.Models;

namespace BreweryAPI.Interfaces
{
    public interface IQuoteDetailRepository
    {
        ICollection<QuoteDetail> GetQuoteDetails();
        QuoteDetail GetQuoteDetail(int id);
        bool QuoteDetailExists(int id);
        bool CreateQuoteDetail(QuoteDetail inventory);
        bool UpdateQuoteDetail(QuoteDetail inventory);
        bool DeleteQuoteDetail(QuoteDetail inventory);
        bool Save();
    }
}
