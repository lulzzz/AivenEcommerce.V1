using System;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Dtos.Products;
using AivenEcommerce.V1.Domain.Services;
using AivenEcommerce.V1.WebApi.Presenter;

using BusinessLogicEnterprise.Application.OperationResults;

using Microsoft.AspNetCore.Mvc;

namespace AivenEcommerce.V1.WebApi.Controllers
{
    [ApiVersion("2.0")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        [HttpPost]
        [ProducesResponseType(typeof(OperationResult<ProductDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> Create(CreateProductInput input)
        {
            var result = await _productService.CreateAsync(input);

            return new OperationActionResult(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(OperationResult<ProductDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> Update(UpdateProductInput input)
        {
            var result = await _productService.UpdateAsync(input);

            return new OperationActionResult(result);
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(OperationResult<ProductDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _productService.DeleteAsync(new DeleteProductInput(id));

            return new OperationActionResult(result);
        }

        [HttpGet("{id:length(24)}")]
        [ProducesResponseType(typeof(OperationResult<ProductDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _productService.GetAsync(id);

            return new OperationActionResult(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(OperationResultEnumerable<ProductDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productService.GetAllAsync();

            return new OperationActionResult(result);
        }
    }
}
