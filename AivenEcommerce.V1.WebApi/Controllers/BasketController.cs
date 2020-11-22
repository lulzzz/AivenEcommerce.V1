
using System;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Dtos.Baskets;
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
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _service;

        public BasketController(IBasketService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet("{customerEmail}")]
        [ProducesResponseType(typeof(OperationResult<BasketDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> GetAllByProduct(string customerEmail)
        {
            var result = await _service.GetBasketAsync(customerEmail);

            return new OperationActionResult(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(OperationResult<BasketDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> AddBasketProduct(AddBasketProductInput input)
        {
            var result = await _service.AddBasketProductAsync(input);

            return new OperationActionResult(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(OperationResult<BasketDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> UpdateBasketAsync(UpdateBasketInput input)
        {
            var result = await _service.UpdateBasketAsync(input);

            return new OperationActionResult(result);
        }

        [HttpDelete("{customerEmail}")]
        [ProducesResponseType(typeof(OperationResult<BasketDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> RemoveAllBasketAsync(string customerEmail)
        {
            var result = await _service.RemoveAllBasketAsync(new(customerEmail));

            return new OperationActionResult(result);
        }

        [HttpDelete("{customerEmail}/products/{productId}")]
        [ProducesResponseType(typeof(OperationResult<BasketDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> RemoveBasketProductAsync(string customerEmail, string productId)
        {
            var result = await _service.RemoveBasketProductAsync(new(productId, customerEmail));

            return new OperationActionResult(result);
        }
    }
}
