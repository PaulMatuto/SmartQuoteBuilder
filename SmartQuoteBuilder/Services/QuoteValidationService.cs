using Microsoft.EntityFrameworkCore;
using Serilog;
using SmartQuoteBuilder.Data;
using SmartQuoteBuilder.Services.Interfaces;

namespace SmartQuoteBuilder.Services
{
    public class QuoteValidationService : IQuoteValidationService
    {
        private readonly ApplicationDbContext _db;
        public QuoteValidationService(ApplicationDbContext db) 
        { 
            _db = db; 
        }
        public async Task ValidateQuoteRequestAsync(int productId, List<int> optionIds)
        {
            // Product must exist
            var productExists = await _db.Products.AnyAsync(p => p.ProductId == productId);
            if (!productExists)
            { 
                Log.Warning("Validation failed: Product {ProductId} does not exist", productId);
                throw new Exception($"Product with ID {productId} does not exist");
            }

            // At least 1 option must be selected
            if (optionIds == null || optionIds.Count == 0)
                throw new Exception("You must select at least one option.");

            // There must be no duplicates
            if (optionIds.Count != optionIds.Distinct().Count())
                throw new Exception("Duplicate option IDs are not allowed.");

            // All options must belong to the product
            var options = await _db.ProductOptions.Where(o => optionIds.Contains(o.OptionId)).ToListAsync();
            if (options.Count != optionIds.Count)
                throw new Exception("One or more selected options are invalid");

            bool productHasWrongOption = options.Any(o => o.ProductId != productId);
            if (productHasWrongOption)
            {
                Log.Warning("Validation failed: Option {OptionId} is not valid for Product {ProductId}", optionIds, productId);
                throw new Exception("Some selected options do not belong to the product.");
            }
        }
    }
}
