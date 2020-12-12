
using PayPalCheckoutSdk.Orders;

using System.Collections.Generic;
using System.Threading.Tasks;

using PaypalOrder = PayPalCheckoutSdk.Orders.Order;

namespace AivenEcommerce.V1.Modules.PayPal.Services
{
    public interface IPayPalService
    {
        Task CancelInvoice(string paypalOrderId);
        Task<PaypalOrder> CaptureOrder(string paypalOrderId);
        Task<PaypalOrder> GetOrder(string paypalOrderId);
        Task<PaypalOrder> CreatePaypalOrder(IEnumerable<PurchaseUnitRequest> purchaseUnits, ApplicationContext applicationContext);
    }
}