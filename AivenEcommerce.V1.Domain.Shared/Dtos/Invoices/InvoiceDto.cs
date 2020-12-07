using AivenEcommerce.V1.Domain.Shared.Enums;

using System;

namespace AivenEcommerce.V1.Domain.Shared.Dtos.Invoices
{
    public record InvoiceDto(Guid Id, Uri Link, string Transaction, string OrderId, bool PaymentProviderWebhookReceived, PaymentProvider PaymentProvider);

}
