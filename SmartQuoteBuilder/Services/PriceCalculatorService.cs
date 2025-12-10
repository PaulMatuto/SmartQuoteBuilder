using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using SmartQuoteBuilder.Data;
using SmartQuoteBuilder.Services.Interfaces;

namespace SmartQuoteBuilder.Services
{
    public class PriceCalculatorService : IPriceCalculatorService
    {
        private readonly ApplicationDbContext _db;
        public PriceCalculatorService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<decimal> CalculateTotalPriceAsync(int productId, List<int> selectedOptionIds)
        {
            Log.Debug("Calculating price for ProductId={productId} Options={selectedOptionIds}",
                        productId, string.Join(",", selectedOptionIds));

            var product = await _db.Products.FirstOrDefaultAsync(p=>p.ProductId == productId);

            if (product == null)
                throw new Exception("Product not found.");

            decimal totalPrice = 0;

            var selectedOptions = await _db.ProductOptions.Where(o => selectedOptionIds.Contains(o.OptionId)).ToListAsync();

            foreach (var option in selectedOptions)
            {
                totalPrice += option.AdditionalPrice;
                Log.Debug("Added option {OptionId} (+{AdditionalPrice})", option.OptionId, option.AdditionalPrice);
            }

            Log.Information("Final price for ProductId={ProductId} = {Total}", productId, totalPrice);

            return totalPrice;
        }
    }
}
