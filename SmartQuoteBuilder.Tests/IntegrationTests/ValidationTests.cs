using Microsoft.Extensions.DependencyInjection;
using SmartQuoteBuilder.Services.Interfaces;
using SmartQuoteBuilder.Tests.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartQuoteBuilder.Tests.IntegrationTests
{
    public class ValidationTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly IQuoteValidationService _validator;
        public ValidationTests(CustomWebApplicationFactory factory)
        {
            var scope = factory.Services.CreateScope();
        }

        [Fact]
        public async Task InvalidProduct_ShouldThrow()
        {
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await _validator.ValidateQuoteRequestAsync(999, [ 1 ] );
            });
        }

        [Fact]
        public async Task InvalidOption_ShouldShow()
        {
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                // Product 1 has options 1 and 2 only
                await _validator.ValidateQuoteRequestAsync(1, [999] );
            });
        }

        [Fact]
        public async Task OptionBelongToAnotherProduct_ShouldShow()
        {
            // Option 3 belongs to Product 2
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await _validator.ValidateQuoteRequestAsync(1, [3]);
            });
        }

        [Fact]
        public async Task EmptyOptionList_ShouldThrow()
        {
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await _validator.ValidateQuoteRequestAsync(1, []);
            });
        }

        [Fact]
        public async Task DuplicateOptions_ShouldThrow()
        {
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await _validator.ValidateQuoteRequestAsync(1, [1, 1]);
            });
        }

        [Fact]
        public async Task ValidRequest_ShouldNotThrow()
        {
            var exception = await Record.ExceptionAsync(async () =>
            {
                await _validator.ValidateQuoteRequestAsync(1, [1, 2]);
            });

            Assert.Null(exception);
        }
    }
}
