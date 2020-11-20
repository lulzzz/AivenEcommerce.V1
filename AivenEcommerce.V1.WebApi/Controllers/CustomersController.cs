using System;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Dtos.Customers;
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
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomersController(ICustomerService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        [ProducesResponseType(typeof(OperationResultEnumerable<CustomerDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();

            return new OperationActionResult(result);
        }

        [HttpGet("{email}")]
        [ProducesResponseType(typeof(OperationResultEnumerable<CustomerDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> Get(string email)
        {
            var result = await _service.GetCustomerAsync(email);

            return new OperationActionResult(result);
        }


        [HttpPost]
        [ProducesResponseType(typeof(OperationResult<CustomerDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> Create(CreateCustomerInput input)
        {
            var result = await _service.CreateCustomerAsync(input);

            return new OperationActionResult(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(OperationResult<CustomerDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> Update(UpdateCustomerInput input)
        {
            var result = await _service.UpdateCustomerAsync(input);

            return new OperationActionResult(result);
        }

        [HttpDelete("{email}")]
        [ProducesResponseType(typeof(OperationResult<CustomerDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> Delete(string email)
        {
            var result = await _service.DeleteCustomerAsync(new(email));

            return new OperationActionResult(result);
        }
    }
}
