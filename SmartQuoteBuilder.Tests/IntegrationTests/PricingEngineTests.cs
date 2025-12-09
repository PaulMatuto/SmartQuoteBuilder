using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using SmartQuoteBuilder.Services.Interfaces;
using SmartQuoteBuilder.Tests.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartQuoteBuilder.Tests.IntegrationTests
{
    public class PricingEngineTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly IPriceCalculatorService _pricingEngine;
        public PricingEngineTests(CustomWebApplicationFactory factory)
        {
            using var scope = factory.Services.CreateScope();
            _pricingEngine = scope.ServiceProvider.GetRequiredService<IPriceCalculatorService>();
        }

        [Fact]
        public async Task CalculateTotalPrice_ShouldReturnCorrectSum()
        {
            // Arrange
            int productId = 1;
            var optionIds = new List<int> { 1, 2 }; // 50 + 120 = 170

            // Act
            var total = await _pricingEngine.CalculateTotalPriceAsync(productId, optionIds);

            // Assert
            Assert.Equal(170, total);
        }

        [Fact]
        public async Task CalculateTotalPrice_WithNoOptions_ShouldReturnZero()
        {
            // Act
            var total = await _pricingEngine.CalculateTotalPriceAsync(1, new List<int>());

            // Assert
            Assert.Equal(0, total);
        }

        [Fact]
        public async Task CalculateTotalPrice_InvalidOption_ShouldIgnoreInvalid()
        {
            // Arrange – option 999 does not exist
            var optionIds = new List<int> { 1, 999 }; // Should count only option 1 → 50

            // Act
            var total = await _pricingEngine.CalculateTotalPriceAsync(1, optionIds);

            // Assert
            Assert.Equal(50, total);
        }
    }
}
