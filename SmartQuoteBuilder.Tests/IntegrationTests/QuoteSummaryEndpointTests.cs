using System.Net;
using System.Net.Http.Json;
using SmartQuoteBuilder.Models;
using SmartQuoteBuilder.Tests.Factory;
using Xunit;

namespace SmartQuoteBuilder.Tests.IntegrationTests
{
    public class QuoteSummaryEndpointTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        public QuoteSummaryEndpointTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task QuoteSummaryEndpoint_ShouldReturnExpandedDetails()
        {
            // Create quote
            var request = new QuoteRequest
            {
                ProductId = 1,
                OptionIds = [1, 2]
            };

            var createResponse = await _client.PostAsJsonAsync("/api/quote", request);
            Assert.Equal(HttpStatusCode.OK, createResponse.StatusCode);

            var createdQuote = await createResponse.Content.ReadFromJsonAsync<Quote>();
            Assert.NotNull(createdQuote);

            int quoteId = createdQuote!.QuoteId;

            // Fetch summary
            var summaryResponse = await _client.GetAsync($"/api/quote/{quoteId}");
            Assert.Equal(HttpStatusCode.OK, summaryResponse.StatusCode);

            var summary = await summaryResponse.Content.ReadFromJsonAsync<QuoteSummaryResponse>();
            Assert.NotNull(summary);

            // Validate summary data
            Assert.Equal(quoteId, summary!.QuoteId);
            Assert.Equal("Laptop X1", summary.ProductName);

            // Option names should match seeded test data
            Assert.Contains("8GB RAM", summary.SelectedOptionNames);
            Assert.Contains("16GB RAM", summary.SelectedOptionNames);

            Assert.Equal(170, summary.TotalPrice);   // pricing engine validated
            Assert.True(summary.CreatedAt <= DateTime.UtcNow);
        }
    }
}
