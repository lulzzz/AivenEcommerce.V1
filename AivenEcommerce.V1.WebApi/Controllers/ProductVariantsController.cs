using System;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Dtos.ProductVariants;
using AivenEcommerce.V1.Domain.OperationResults;
using AivenEcommerce.V1.Domain.Services;
using AivenEcommerce.V1.WebApi.Presenter;

using Microsoft.AspNetCore.Mvc;

namespace AivenEcommerce.V1.WebApi.Controllers
{
    [ApiVersion("2.0")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ProductVariantsController : ControllerBase
    {
        private readonly IProductVariantService _service;

        public ProductVariantsController(IProductVariantService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet("products/{productId}")]
        [ProducesResponseType(typeof(OperationResultEnumerable<ProductVariantDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> GetAllByProduct(string productId)
        {
            var result = await _service.GetAllAsync(productId);

            return new OperationActionResult(result);
        }

        [HttpGet("{name}/products/{productId}")]
        [ProducesResponseType(typeof(OperationResultEnumerable<ProductVariantDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> GetAllByProduct(string name, string productId)
        {
            var result = await _service.GetAsync(productId, name);

            return new OperationActionResult(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(OperationResult<ProductVariantDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> Create(CreateProductVariantInput input)
        {
            var result = await _service.CreateAsync(input);

            return new OperationActionResult(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(OperationResult<ProductVariantDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> Update(UpdateProductVariantInput input)
        {
            var result = await _service.UpdateAsync(input);

            return new OperationActionResult(result);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(OperationResult<ProductVariantDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> Delete(DeleteProductVariantInput input)
        {
            var result = await _service.DeleteAsync(input);

            return new OperationActionResult(result);
        }
    }
}
