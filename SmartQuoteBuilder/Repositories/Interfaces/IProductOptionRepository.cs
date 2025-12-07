using SmartQuoteBuilder.Models;

namespace SmartQuoteBuilder.Repositories.Interfaces
{
    public interface IProductOptionRepository
    {
        Task<List<ProductOption>> GetOptionsByProductIdAsync(int productId);
    }
}
