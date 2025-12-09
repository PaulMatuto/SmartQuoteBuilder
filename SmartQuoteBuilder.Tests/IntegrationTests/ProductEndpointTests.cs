using SmartQuoteBuilder.Models;
using SmartQuoteBuilder.Tests.Factory;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace SmartQuoteBuilder.Tests.IntegrationTests
{
    public class ProductEndpointTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        public ProductEndpointTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetProducts_ShouldReturnOk_WithJsonList()
        {
            // Act
            var response = await _client.GetAsync("/api/products");

            // Assert: 200 OK
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // Assert content type is JSON
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType!.ToString());

            // Assert: Should return a list even when empty
            var products = await response.Content.ReadFromJsonAsync<List<Product>>();
            Assert.NotNull(products);
        }

        [Fact]
        public async Task Products_ShouldNotBeEmpty()
        {
            // Act
            var response = await _client.GetAsync("/api/products");
            var list = await response.Content.ReadFromJsonAsync<List<Product>>();

            // Assert
            Assert.True(list!.Count >= 2);  // We seeded 2 products in Task 16
        }
    }
}
