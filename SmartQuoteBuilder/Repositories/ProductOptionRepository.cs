using SmartQuoteBuilder.Data;

using Microsoft.EntityFrameworkCore;
using SmartQuoteBuilder.Models;
using SmartQuoteBuilder.Repositories.Interfaces;

namespace SmartQuoteBuilder.Repositories;

public class ProductOptionRepository : IProductOptionRepository
{
    private readonly ApplicationDbContext _db;
    public ProductOptionRepository(ApplicationDbContext db)
    {
        _db = db; 
    }
    public async Task<List<ProductOption>> GetOptionsByProductIdAsync(int productId)
    {
        return await _db.ProductOptions.Where(o => o.ProductId == productId).ToListAsync();
    }
}
