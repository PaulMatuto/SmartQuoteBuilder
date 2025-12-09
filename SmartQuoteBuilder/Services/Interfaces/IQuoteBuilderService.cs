using SmartQuoteBuilder.Models;

namespace SmartQuoteBuilder.Services.Interfaces
{
    public interface IQuoteBuilderService
    {
        Task<Quote> BuildQuoteAsync(int producId, List<int> optionIds);
        Task<QuoteSummaryResponse> GetQuoteSummaryAsync(int quoteId);
    }
}
