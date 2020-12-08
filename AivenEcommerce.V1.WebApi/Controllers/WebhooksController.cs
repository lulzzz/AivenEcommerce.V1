using AivenEcommerce.V1.Domain.Services;
using AivenEcommerce.V1.Infrastructure.Options.ClientConfig;

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
    public class WebhooksController : ControllerBase
    {
        private readonly IWebhookService _webhookService;
        private readonly IClientConfigOptions _clientConfigOptions;

        public WebhooksController(IWebhookService webhookService, IClientConfigOptions clientConfigOptions)
        {
            _webhookService = webhookService ?? throw new ArgumentNullException(nameof(webhookService));
            _clientConfigOptions = clientConfigOptions ?? throw new ArgumentNullException(nameof(clientConfigOptions));
        }

        [HttpGet("[action]/{orderId}")]
        public async Task<IActionResult> WebhookReturnPaypal(string orderId, [FromQuery] string token)
        {
            var result = await _webhookService.InvoiceWebhookPayPal(orderId, token);
            return Redirect(_clientConfigOptions.WebhookReturnUrl);
        }

        [HttpGet("[action]")]
        public IActionResult WebhookCancelMobbex([FromQuery] string token)
        {
            return Redirect(_clientConfigOptions.WebhookCancelUrl);
        }
    }
}
