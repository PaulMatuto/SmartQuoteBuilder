using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using SmartQuoteBuilder.Models;
using SmartQuoteBuilder.Services.Interfaces;

namespace SmartQuoteBuilder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteBuilderService _quoteBuilderService;
        public QuoteController(IQuoteBuilderService quoteBuilderService)
        {
            _quoteBuilderService = quoteBuilderService;
        }

        // POST: api/quote
        [HttpPost]
        public async Task<IActionResult> CreateQuote([FromBody] QuoteRequest request)
        {
            Log.Information("Received quote request for ProductId={ProductId}", request.ProductId);

            if (request == null || request.OptionIds == null)
                return BadRequest("Invalid quote request.");

            var quote = await _quoteBuilderService.BuildQuoteAsync(request.ProductId, request.OptionIds);

            return Ok(quote);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuoteSummary(int id)
        {
            var summary = await _quoteBuilderService.GetQuoteSummaryAsync(id);
            Log.Information("Returning summary for QuoteId={QuoteId}", id);

            return Ok(summary);
        }
    }
}
