using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartQuoteBuilder.Models;

public class CreateQuoteModel : PageModel
{
    private readonly IHttpClientFactory _factory;
    public CreateQuoteModel(IHttpClientFactory factory)
    {
        _factory = factory;
    }

    [BindProperty]
    public QuoteRequest RequestModel { get; set; } = new();
    public List<ProductOption> Options { get; set; } = new();

    public async Task OnGet(int productId)
    {
        RequestModel.ProductId = productId;
        var client = _factory.CreateClient("api");
        Options = await client.GetFromJsonAsync<List<ProductOption>>($"api/ProductOptions?productId={productId}");
    }

    public async Task<IActionResult> OnPost()
    {
        var client = _factory.CreateClient("api");
        var response = await client.PostAsJsonAsync("/api/quote", RequestModel);
        var quote = await response.Content.ReadFromJsonAsync<Quote>();

        return Redirect($"/Quotes/Summary?id={quote!.QuoteId}");
    }
}