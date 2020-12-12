
using AivenEcommerce.V1.Domain.Entities;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Factories.PaymentProviders
{
    public interface IPaymentProvider
    {
        Task CancelInvoice(Invoice invoice);
        Task ConfirmOrder(Invoice invoice);
        Task<Invoice> CreateInvoice(Order order);
        Task<Invoice> UpdateInvoice(Invoice invoice, Order order);
    }
}
