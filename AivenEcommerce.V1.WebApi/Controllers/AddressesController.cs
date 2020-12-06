using AivenEcommerce.V1.Domain.Services;
using AivenEcommerce.V1.Domain.Shared.Dtos.Addresses;
using AivenEcommerce.V1.Domain.Shared.OperationResults;
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
    public class AddressesController : ControllerBase
    {
        private readonly IAddressService _service;

        public AddressesController(IAddressService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet("{customerEmail}")]
        [ProducesResponseType(typeof(OperationResult<AddressDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> GetAll(string customerEmail)
        {
            var result = await _service.GetAllAsync(customerEmail);

            return new OperationActionResult(result);
        }

        [HttpGet("{customerEmail}/{addressId}")]
        [ProducesResponseType(typeof(OperationResult<AddressDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> Get(string customerEmail, Guid addressId)
        {
            var result = await _service.GetAddressAsync(addressId, customerEmail);

            return new OperationActionResult(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(OperationResult<AddressDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> Create(CreateAddressInput input)
        {
            var result = await _service.CreateAddressAsync(input);

            return new OperationActionResult(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(OperationResult<AddressDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> UpdateAddressAsync(UpdateAddressInput input)
        {
            var result = await _service.UpdateAddressAsync(input);

            return new OperationActionResult(result);
        }


        [HttpDelete("{customerEmail}/{addressId}")]
        [ProducesResponseType(typeof(OperationResult<AddressDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> RemoveAddressAsync(string customerEmail, Guid addressId)
        {
            var result = await _service.DeleteAddressAsync(new(addressId, customerEmail));

            return new OperationActionResult(result);
        }
    }
}
