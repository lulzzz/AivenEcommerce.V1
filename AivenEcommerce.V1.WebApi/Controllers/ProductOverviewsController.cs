using AivenEcommerce.V1.Domain.Dtos.ProductOverViews;
using AivenEcommerce.V1.Domain.OperationResults;
using AivenEcommerce.V1.Domain.Services;
using AivenEcommerce.V1.WebApi.Presenter;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.WebApi.Controllers
{
    [ApiVersion("2.0")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ProductOverviewsController : ControllerBase
    {
        private readonly IProductOverviewService _service;

        public ProductOverviewsController(IProductOverviewService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet("product/{productId:length(24)}")]
        [ProducesResponseType(typeof(OperationResult<ProductOverviewDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> Get(string productId)
        {
            var result = await _service.GetByProductAsync(productId);

            return new OperationActionResult(result);
        }
    }
}
