using AivenEcommerce.V1.Domain.Services;
using AivenEcommerce.V1.Domain.Shared.Dtos.Orders;
using AivenEcommerce.V1.Domain.Shared.Dtos.Sales;
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
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SalesController(ISaleService saleService)
        {
            _saleService = saleService ?? throw new ArgumentNullException(nameof(saleService));
        }

        [HttpPost]
        [ProducesResponseType(typeof(OperationResult<SaleOrderDto>), 200)]
        [ProducesResponseType(typeof(OperationResult), 400)]
        [ProducesResponseType(typeof(OperationResult), 500)]
        public async Task<IActionResult> Create(CreateSaleInput input)
        {
            var result = await _saleService.CreateSaleAsync(input);
            return new OperationActionResult(result);
        }
    }
}
