using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartQuoteBuilder.Repositories.Interfaces;

namespace SmartQuoteBuilder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductOptionsController : ControllerBase
    {
        private readonly IProductOptionRepository _productOptionRepository;
        public ProductOptionsController(IProductOptionRepository productOptionRepository)
        {
            _productOptionRepository = productOptionRepository;
        }

        // GET: api/products/*productId*/options
        [HttpGet]
        public async Task<IActionResult> GetOptionsByProductId(int productId)
        {
            var options = await _productOptionRepository.GetOptionsByProductIdAsync(productId);

            if(options == null || options.Count == 0)
            {
                return NotFound(new { message = "No options found for this product."});
            }

            return Ok(options);
        }
    }
}
