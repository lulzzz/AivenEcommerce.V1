using AivenEcommerce.V1.Domain.Dtos.CouponCodes;
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
    public class CouponCodesController : ControllerBase
    {
        private readonly ICouponCodeService _service;

        public CouponCodesController(ICouponCodeService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        [ProducesResponseType(typeof(OperationResultEnumerable<CouponCodeDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetCouponCodeAsync();

            return new OperationActionResult(result);
        }

        [HttpGet("{code}")]
        [ProducesResponseType(typeof(OperationResult<CouponCodeDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> Get(string code)
        {
            var result = await _service.GetCouponCodeAsync(code);

            return new OperationActionResult(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(OperationResult<CouponCodeDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> Create(CreateCouponCodeInput input)
        {
            var result = await _service.CreateCouponCodeAsync(input);

            return new OperationActionResult(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(OperationResult<CouponCodeDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> Update(UpdateCouponCodeInput input)
        {
            var result = await _service.UpdateCouponCodeAsync(input);

            return new OperationActionResult(result);
        }

        [HttpDelete("{code}")]
        [ProducesResponseType(typeof(OperationResult), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> Delete(string code)
        {
            var result = await _service.RemoveCouponCodeAsync(new(code));

            return new OperationActionResult(result);
        }
    }
}
