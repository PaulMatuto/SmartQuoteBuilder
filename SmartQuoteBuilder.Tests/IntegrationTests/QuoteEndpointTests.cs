using Microsoft.AspNetCore.Mvc.Testing;
using SmartQuoteBuilder.Models;
using SmartQuoteBuilder.Tests.Factory;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace SmartQuoteBuilder.Tests.IntegrationTests
{
    public class QuoteEndpointTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        public QuoteEndpointTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateQuote_ShouldReturnQuote_WithCorrectTotal()
        {
            // Arrange
            var request = new QuoteRequest
            {
                ProductId = 1,
                OptionIds = new List<int> { 1, 2 }  // 50 + 120 = 170
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/quote", request);

            // Assert status code
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // Read quote
            var quote = await response.Content.ReadFromJsonAsync<Quote>();

            // Validate returned quote
            Assert.NotNull(quote);
            Assert.Equal(1, quote!.ProductId);
            Assert.Equal("1,2", quote.SelectedOptionIds);
            Assert.Equal(170, quote.TotalPrice);  // expected from seeded data
        }
    }
}
