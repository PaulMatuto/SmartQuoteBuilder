using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartQuoteBuilder.Models;

public class ProductsIndexModel : PageModel
{
    private readonly IHttpClientFactory _factory;
    public ProductsIndexModel(IHttpClientFactory factory)
    {
        _factory = factory;
    }
    public List<Product> Products { get; set; } = new();
    public async Task OnGet()
    {
        var client = _factory.CreateClient("api");
        Products = await client.GetFromJsonAsync<List<Product>>("/api/products");
    }
}