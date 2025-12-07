using SmartQuoteBuilder.Models;

namespace SmartQuoteBuilder.Repositories.Interfaces
{
    public interface IQuoteRepository
    {
        Task<Quote> CreateQuoteAsync(Quote quote);
        Task<Quote> GetQuoteByIdAsync(int quoteId);
    }
}
