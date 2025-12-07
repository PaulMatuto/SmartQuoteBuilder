using Microsoft.EntityFrameworkCore;
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
            var product = await _db.Products.FirstOrDefaultAsync(p=>p.ProductId == productId);

            if (product == null)
                throw new Exception("Product not found.");

            decimal totalPrice = 0;

            var selectedOptions = await _db.ProductOptions.Where(o => selectedOptionIds.Contains(o.OptionId)).ToListAsync();

            foreach (var option in selectedOptions)
            {
                totalPrice += option.AdditionalPrice;
            }

            return totalPrice;
        }
    }
}
