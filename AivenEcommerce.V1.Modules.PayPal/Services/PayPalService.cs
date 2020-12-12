using AivenEcommerce.V1.Modules.PayPal.Enum;
using AivenEcommerce.V1.Modules.PayPal.Options;
using AivenEcommerce.V1.Modules.PayPal.Request;

using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;

using PayPalHttp;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PaypalOrder = PayPalCheckoutSdk.Orders.Order;

namespace AivenEcommerce.V1.Modules.PayPal.Services
{
    public class PayPalService : IPayPalService
    {
        private readonly IPayPalOptions _paypalOptions;

        public PayPalService(IPayPalOptions paypalOptions)
        {
            _paypalOptions = paypalOptions ?? throw new ArgumentNullException(nameof(paypalOptions));
        }

        public async Task CancelInvoice(string paypalOrderId)
        {
            PayPalCheckoutSdk.Core.PayPalEnvironment environment = CreateEnvironment();
            var client = new PayPalHttpClient(environment);

            var request = new OrderDeleteRequest(paypalOrderId);

            try
            {
                await client.Execute(request);
            }
            catch (HttpException httpException)
            {
                var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();
            }
        }

        public async Task<PaypalOrder> CreatePaypalOrder(IEnumerable<PurchaseUnitRequest> purchaseUnits, ApplicationContext applicationContext)
        {
            PayPalCheckoutSdk.Core.PayPalEnvironment environment = CreateEnvironment();
            var client = new PayPalHttpClient(environment);

            var payment = new OrderRequest()
            {
                CheckoutPaymentIntent = "CAPTURE",
                PurchaseUnits = purchaseUnits.ToList(),
                ApplicationContext = applicationContext
            };

            //https://developer.paypal.com/docs/api/orders/v2/#orders_create
            var request = new OrdersCreateRequest();
            request.Prefer("return=representation");
            request.RequestBody(payment);

            try
            {
                var response = await client.Execute(request);
                var result = response.Result<PaypalOrder>();

                return result;
            }
            catch (HttpException httpException)
            {
                var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();
                throw httpException;
            }
        }

    
        public async Task<PaypalOrder> CaptureOrder(string transaction)
        {
            PayPalCheckoutSdk.Core.PayPalEnvironment environment = CreateEnvironment();
            var client = new PayPalHttpClient(environment);

            var request = new OrdersCaptureRequest(transaction);
            request.Prefer("return=representation");
            request.RequestBody(new OrderActionRequest());
            try
            {
                HttpResponse response = await client.Execute(request);
                PaypalOrder order = response.Result<PaypalOrder>();

                return order;
            }
            catch (HttpException httpException)
            {
                var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();
                throw httpException;
            }
        }

        private PayPalCheckoutSdk.Core.PayPalEnvironment CreateEnvironment()
        {
            return _paypalOptions.Environment switch
            {
                Enum.PayPalEnvironment.Live => new LiveEnvironment(_paypalOptions.ClientId, _paypalOptions.ClientSecret),
                Enum.PayPalEnvironment.Sandbox => new SandboxEnvironment(_paypalOptions.ClientId, _paypalOptions.ClientSecret),
                _ => throw new ArgumentException()
            };
        }

        public async Task<PaypalOrder> GetOrder(string paypalOrderId)
        {
            PayPalCheckoutSdk.Core.PayPalEnvironment environment = CreateEnvironment();
            var client = new PayPalHttpClient(environment);

            var request = new OrdersGetRequest(paypalOrderId);

            try
            {
                HttpResponse response = await client.Execute(request);
                PaypalOrder order = response.Result<PaypalOrder>();

                return order;
            }
            catch (HttpException httpException)
            {
                var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();
                throw httpException;
            }
        }
    }
}
