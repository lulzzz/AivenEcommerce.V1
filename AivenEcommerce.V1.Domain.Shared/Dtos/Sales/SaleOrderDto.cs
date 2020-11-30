using AivenEcommerce.V1.Domain.Shared.Dtos.Invoices;
using AivenEcommerce.V1.Domain.Shared.Dtos.Orders;

namespace AivenEcommerce.V1.Domain.Shared.Dtos.Sales
{
    public record SaleOrderDto(SaleDto Sale, OrderDto Order, InvoiceDto Invoice);
}
