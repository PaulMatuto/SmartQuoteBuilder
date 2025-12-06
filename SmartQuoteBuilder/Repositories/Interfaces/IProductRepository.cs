using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using Microsoft.EntityFrameworkCore;

using SmartQuoteBuilder.Models;
using Product = SmartQuoteBuilder.Models.Product;

namespace SmartQuoteBuilder.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync();
    }
}
