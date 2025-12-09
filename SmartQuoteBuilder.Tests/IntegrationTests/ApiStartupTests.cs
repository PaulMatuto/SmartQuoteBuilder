using SmartQuoteBuilder.Tests.Factory;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SmartQuoteBuilder.Tests.IntegrationTests
{
    public class ApiStartupTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        public ApiStartupTests(CustomWebApplicationFactory factory) 
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ProductsEndpoint_ShouldReturnSuccess()
        {
            var response = await _client.GetAsync("/api/products");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
