using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace AivenEcommerce.V1.WebApi.Controllers
{
    [ApiVersion("2.0")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class WebhooksController : ControllerBase
    {
        public async Task<IActionResult> WebhookPayPal(string orderId)
        {
            return Ok();
        }
    }
}
