using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using SmartQuoteBuilder.Models;
using Product = SmartQuoteBuilder.Models.Product;

namespace SmartQuoteBuilder.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOption> ProductOptions { get; set; }
        public DbSet<Quote> Quotes { get; set; }
    }
}
