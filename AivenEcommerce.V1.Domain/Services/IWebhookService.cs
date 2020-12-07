using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Services
{
    public interface IWebhookService
    {
        Task InvoicePayPalPayed(string transaction);
    }
}
