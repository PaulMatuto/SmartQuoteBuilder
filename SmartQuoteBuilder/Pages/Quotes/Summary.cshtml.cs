using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartQuoteBuilder.Models;

public class QuoteSummaryPageModel : PageModel
{
    private readonly IHttpClientFactory _factory;
    public QuoteSummaryPageModel(IHttpClientFactory factory)
    {
        _factory = factory;
    }

    public QuoteSummaryResponse Summary {  get; set; }
    public async Task OnGet(int id)
    {
        var client = _factory.CreateClient("api");
        Summary = await client.GetFromJsonAsync<QuoteSummaryResponse>($"/api/quote/{id}");
    }
}
