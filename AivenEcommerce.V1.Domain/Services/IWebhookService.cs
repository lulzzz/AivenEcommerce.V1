using AivenEcommerce.V1.Domain.Shared.Common;
using AivenEcommerce.V1.Domain.Shared.OperationResults;

using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Services
{
    public interface IWebhookService : IScopedService
    {
        Task<OperationResult> InvoiceWebhookPayPal(string orderId, string token);
    }
}
