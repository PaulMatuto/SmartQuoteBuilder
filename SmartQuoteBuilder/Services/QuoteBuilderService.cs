using SmartQuoteBuilder.Models;
using SmartQuoteBuilder.Repositories.Interfaces;
using SmartQuoteBuilder.Services.Interfaces;

namespace SmartQuoteBuilder.Services
{
    public class QuoteBuilderService : IQuoteBuilderService
    {
        private readonly IPriceCalculatorService _priceCalculator;
        private readonly IQuoteRepository _quoteRepository;
        public QuoteBuilderService(IPriceCalculatorService priceCalculator, IQuoteRepository quoteRepository)
        {
            _priceCalculator = priceCalculator;
            _quoteRepository = quoteRepository;
        }
        public async Task<Quote> BuildQuoteAsync(int productId, List<int> optionIds)
        {
            // Calculate final price using pricing service
            decimal totalPrice = await _priceCalculator.CalculateTotalPriceAsync(productId, optionIds);

            // Convert option list to CSV for storage
            string optionString = string.Join(",", optionIds);

            // Create Quote Object
            var newQuote = new Quote { ProductId = productId, SelectedOptionIds = optionString, TotalPrice = totalPrice };

            // Save the quote in Database
            var createdQuote = await _quoteRepository.CreateQuoteAsync(newQuote);

            // Return completed quote
            return createdQuote;
        }
    }
}
