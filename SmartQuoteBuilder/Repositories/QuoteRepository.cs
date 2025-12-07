using Microsoft.EntityFrameworkCore;
using SmartQuoteBuilder.Data;
using SmartQuoteBuilder.Models;
using SmartQuoteBuilder.Repositories.Interfaces;

namespace SmartQuoteBuilder.Repositories
{
    public class QuoteRepository : IQuoteRepository
    {
        private readonly ApplicationDbContext _db;
        public QuoteRepository(ApplicationDbContext db)
        {
            _db= db;
        }
        public async Task<Quote> CreateQuoteAsync(Quote quote)
        {
            _db.Quotes.Add(quote);
            await _db.SaveChangesAsync();

            return quote;
        }
        public async Task<Quote> GetQuoteByIdAsync(int quoteId)
        {
            return await _db.Quotes.FirstOrDefaultAsync(q => q.QuoteId == quoteId);
        }
    }
}
