using SmartQuoteBuilder.Models;
using SmartQuoteBuilder.Tests.Factory;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace SmartQuoteBuilder.Tests.IntegrationTests
{
    public class OptionEndpointTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        public OptionEndpointTests(CustomWebApplicationFactory factory)
        { 
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetOptions_ShouldReturnOk_WithJsonList()
        {
            // Arrange
            int productId = 1; // still return 200 OK response even with empty list

            // Act
            var response = await _client.GetAsync($"/api/options/{productId}");

            // Assert: 200 OK
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // Assert content type is JSON
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType!.ToString());

            // Assert: Should return a list even when empty
            var options = await response.Content.ReadFromJsonAsync<List<ProductOption>>();
            Assert.NotNull(options);
        }
    }
}
