using AivenEcommerce.V1.Domain.Dtos.Products;
using AivenEcommerce.V1.Domain.OperationResults;
using AivenEcommerce.V1.Domain.Paginations;
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
        [ProducesResponseType(typeof(OperationResult<PagedResult<ProductDto>>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> GetAll([FromQuery] ProductParameters parameters)
        {
            var result = await _productService.GetAllAsync(parameters);

            return new OperationActionResult(result);
        }

        [HttpGet("categories/{category}")]
        [ProducesResponseType(typeof(OperationResultEnumerable<ProductDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public IActionResult GetByCategory(string category)
        {
            var result = _productService.GetByCategory(category);

            return new OperationActionResult(result);
        }

        [HttpGet("categories/{category}/subcategories/{subcategory}")]
        [ProducesResponseType(typeof(OperationResultEnumerable<ProductDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public IActionResult GetByCategory(string category, string subcategory)
        {
            var result = _productService.GetByCategory(category, subcategory);

            return new OperationActionResult(result);
        }

        [HttpPut("[action]")]
        [ProducesResponseType(typeof(OperationResult<ProductDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> UpdateMainImage(UpdateProductMainImageInput input)
        {
            var result = await _productService.UpdateMainImageAsync(input);

            return new OperationActionResult(result);
        }

        [HttpPut("[action]")]
        [ProducesResponseType(typeof(OperationResult<ProductDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> UpdateCostPrice(UpdateProductCostPriceInput input)
        {
            var result = await _productService.UpdateProductCostPriceAsync(input);

            return new OperationActionResult(result);
        }

        [HttpPut("[action]")]
        [ProducesResponseType(typeof(OperationResult<ProductDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> UpdateCategory(UpdateProductCategorySubCategoryInput input)
        {
            var result = await _productService.UpdateProductCategoryAsync(input);

            return new OperationActionResult(result);
        }

        [HttpPut("[action]")]
        [ProducesResponseType(typeof(OperationResult<ProductDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> UpdateAvailability(UpdateProductAvailabilityInput input)
        {
            var result = await _productService.UpdateProductAvailabilityAsync(input);

            return new OperationActionResult(result);
        }

        [HttpPut("[action]")]
        [ProducesResponseType(typeof(OperationResult<ProductDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> UpdateNameDescription(UpdateProductNameDescriptionInput input)
        {
            var result = await _productService.UpdateProductNameDescriptionAsync(input);

            return new OperationActionResult(result);
        }

        [HttpPut("[action]")]
        [ProducesResponseType(typeof(OperationResult<ProductDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> UpdateBadge(UpdateProductBadgeInput input)
        {
            var result = await _productService.UpdateProductBadgeAsync(input);

            return new OperationActionResult(result);
        }
    }
}
