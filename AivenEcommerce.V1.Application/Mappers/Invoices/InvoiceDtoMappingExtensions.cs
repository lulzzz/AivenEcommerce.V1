using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Shared.Dtos.Invoices;

namespace AivenEcommerce.V1.Application.Mappers.Invoices
{
    public static class InvoiceDtoMappingExtensions
    {
        public static InvoiceDto ConvertToDto(this Invoice source)
        {
            return new(source.Id, source.Link, source.Transaction, source.OrderId, source.PaymentProviderWebhookReceived, source.PaymentProvider);
        }
    }
}
