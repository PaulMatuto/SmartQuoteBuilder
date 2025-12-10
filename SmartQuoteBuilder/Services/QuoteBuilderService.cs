using Microsoft.EntityFrameworkCore;
using SmartQuoteBuilder.Data;
using SmartQuoteBuilder.Models;
using SmartQuoteBuilder.Repositories.Interfaces;
using SmartQuoteBuilder.Services.Interfaces;
using Serilog;

namespace SmartQuoteBuilder.Services
{
    public class QuoteBuilderService : IQuoteBuilderService
    {
        private readonly IPriceCalculatorService _priceCalculator;
        private readonly IQuoteRepository _quoteRepository;
        private readonly IQuoteValidationService _quoteValidationService;
        private readonly ApplicationDbContext _db;
        public QuoteBuilderService(IPriceCalculatorService priceCalculator, IQuoteRepository quoteRepository,
            IQuoteValidationService quoteValidationService, ApplicationDbContext db)
        {
            _priceCalculator = priceCalculator;
            _quoteRepository = quoteRepository;
            _quoteValidationService = quoteValidationService;
            _db = db;
        }
        public async Task<Quote> BuildQuoteAsync(int productId, List<int> optionIds)
        {
            Log.Information("Starting quote build for Product ID: {productId} Options: {optionIds}",
                            productId, string.Join(",", optionIds));

            // Quote request validation
            await _quoteValidationService.ValidateQuoteRequestAsync(productId, optionIds);
            Log.Information("Validation successful for ProductId={ProductId}", productId);

            // Calculate final price using pricing service
            decimal totalPrice = await _priceCalculator.CalculateTotalPriceAsync(productId, optionIds);
            Log.Information("Price calculated: {TotalPrice}", totalPrice);

            // Convert option list to CSV for storage
            string optionString = string.Join(",", optionIds);

            // Create Quote Object
            var newQuote = new Quote { ProductId = productId, SelectedOptionIds = optionString, TotalPrice = totalPrice };

            // Save the quote in Database
            var createdQuote = await _quoteRepository.CreateQuoteAsync(newQuote);
            Log.Information("Quote created with QuoteId={QuoteId}", createdQuote.QuoteId);

            // Return completed quote
            return createdQuote;
        }

        public async Task<QuoteSummaryResponse> GetQuoteSummaryAsync(int quoteId)
        {
            var quote = await _quoteRepository.GetQuoteByIdAsync(quoteId);
            if (quote == null)
                throw new Exception($"Quote with ID {quoteId} does not exist.");

            // Load product
            var product = await _db.Products.FirstOrDefaultAsync(p => p.ProductId == quote.ProductId);

            // Parse options
            var optionIds = quote.SelectedOptionIds.Split(',').Select(int.Parse).ToList();

            var options = await _db.ProductOptions.Where(o => optionIds.Contains(o.OptionId)).ToListAsync();

            return new QuoteSummaryResponse
            {
                QuoteId = quote.QuoteId,
                ProductName = product!.Name,
                SelectedOptionNames = options.Select(o => o.Name).ToList(),
                TotalPrice = quote.TotalPrice,
                CreatedAt = quote.CreatedAt
            };
        }
    }
}
