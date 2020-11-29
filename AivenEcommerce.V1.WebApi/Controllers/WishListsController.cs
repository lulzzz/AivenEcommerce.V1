using AivenEcommerce.V1.Domain.Dtos.WishLists;
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
    public class WishListsController : ControllerBase
    {
        private readonly IWishListService _service;

        public WishListsController(IWishListService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet("{customerEmail}")]
        [ProducesResponseType(typeof(OperationResult<WishListDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> Get(string customerEmail)
        {
            var result = await _service.GetWishListAsync(customerEmail);

            return new OperationActionResult(result);
        }

        [HttpGet("{customerEmail}/products")]
        [ProducesResponseType(typeof(OperationResult<WishListProductsDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> GetProductInfo(string customerEmail)
        {
            var result = await _service.GetWishListWithProductInfoAsync(customerEmail);

            return new OperationActionResult(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(OperationResult<WishListDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> AddWishListProduct(AddProductToWishListInput input)
        {
            var result = await _service.AddProductToWishListAsync(input);

            return new OperationActionResult(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(OperationResult<WishListDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> UpdateWishListAsync(UpdateWishListInput input)
        {
            var result = await _service.UpdateWishListAsync(input);

            return new OperationActionResult(result);
        }

        [HttpDelete("{customerEmail}")]
        [ProducesResponseType(typeof(OperationResult<WishListDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> RemoveAllWishListAsync(string customerEmail)
        {
            var result = await _service.RemoveAllWishListAsync(new(customerEmail));

            return new OperationActionResult(result);
        }

        [HttpDelete("{customerEmail}/products/{productId}")]
        [ProducesResponseType(typeof(OperationResult<WishListDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> RemoveWishListProductAsync(string customerEmail, string productId)
        {
            var result = await _service.RemoveProductToWishListAsync(new(customerEmail, productId));

            return new OperationActionResult(result);
        }
    }
}
