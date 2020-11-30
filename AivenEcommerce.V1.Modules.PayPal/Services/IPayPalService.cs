using AivenEcommerce.V1.Modules.PayPal.Enum;

using System;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Modules.PayPal.Services
{
    public interface IPayPalService
    {
        Task CancelInvoice(string transaction);
        Task<Guid> ConfirmOrder(string token);
        Task<Uri> CreateUriForPayment(string customId, Currency currency, string description, int totalAmount);
        Task<Uri> UpdateAmountInvoice(string transaction, int totalAmount);
    }
}