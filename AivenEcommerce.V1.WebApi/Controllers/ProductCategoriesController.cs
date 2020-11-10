using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Dtos.ProductCategories;
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
    public class ProductCategoriesController : ControllerBase
    {
        private readonly IProductCategoryService _service;

        public ProductCategoriesController(IProductCategoryService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        [ProducesResponseType(typeof(OperationResultEnumerable<ProductCategoryDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();

            return new OperationActionResult(result);
        }


        [HttpPost]
        [ProducesResponseType(typeof(OperationResult<ProductCategoryDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> Create(CreateProductCategoryInput input)
        {
            var result = await _service.CreateAsync(input);

            return new OperationActionResult(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(OperationResult<ProductCategoryDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> Update(UpdateProductCategoryInput input)
        {
            var result = await _service.UpdateAsync(input);

            return new OperationActionResult(result);
        }

        [HttpGet("{name}")]
        [ProducesResponseType(typeof(OperationResult<ProductCategoryDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> Get(string name)
        {
            var result = await _service.GetAsync(name);

            return new OperationActionResult(result);
        }

        [HttpDelete("{name}")]
        [ProducesResponseType(typeof(OperationResult<ProductCategoryDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> Delete(string name)
        {
            var result = await _service.DeleteAsync(new DeleteProductCategoryInput(name));

            return new OperationActionResult(result);
        }
    }
}
