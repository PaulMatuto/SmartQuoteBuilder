using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartQuoteBuilder;
using SmartQuoteBuilder.Data;
using SmartQuoteBuilder.Models;
using System.Linq;

namespace SmartQuoteBuilder.Tests.Factory
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove existing DB context configuration
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                // Add in-memory DB for testing
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDB");
                });

                // Build the service provider
                var serviceProvider = services.BuildServiceProvider();

                using (var scope = serviceProvider.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    db.Database.EnsureCreated();

                    // Send sample data
                    SeedTestData(db);
                }
            });
        }
        private void SeedTestData(ApplicationDbContext db)
        {
            // ---- PRODUCTS ----
            var p1 = new Product { ProductId = 1, Name = "Laptop X1", Description = "Test laptop" };
            var p2 = new Product { ProductId = 2, Name = "Gaming PC Z7", Description = "Test PC" };

            db.Products.AddRange(p1, p2);

            // ---- OPTIONS ----
            db.ProductOptions.AddRange(
                new ProductOption { OptionId = 1, ProductId = 1, Name = "8GB RAM", AdditionalPrice = 50 },
                new ProductOption { OptionId = 2, ProductId = 1, Name = "16GB RAM", AdditionalPrice = 120 },
                new ProductOption { OptionId = 3, ProductId = 2, Name = "RGB Lighting", AdditionalPrice = 30 }
            );

            db.SaveChanges();
        }
    }
}
