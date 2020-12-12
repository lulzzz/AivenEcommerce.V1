using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Factories.PaymentProviders;
using AivenEcommerce.V1.Domain.Shared.Enums;
using AivenEcommerce.V1.Infrastructure.Options.PaymentProvider;
using AivenEcommerce.V1.Modules.PayPal.Services;

using PayPalCheckoutSdk.Orders;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using PaypalOrder = PayPalCheckoutSdk.Orders.Order;

namespace AivenEcommerce.V1.Infrastructure.Factories.PaymentProviders
{
    public class PayPalPaymentProvider : IPaymentProvider
    {
        private readonly IPayPalService _paypalService;
        private readonly IPaymentProviderOptions _paymentProviderOptions;

        public PayPalPaymentProvider(IPayPalService paypalService, IPaymentProviderOptions paymentProviderOptions)
        {
            _paypalService = paypalService ?? throw new ArgumentNullException(nameof(paypalService));
            _paymentProviderOptions = paymentProviderOptions ?? throw new ArgumentNullException(nameof(paymentProviderOptions));
        }

        public Task CancelInvoice(Invoice invoice)
        {
            return _paypalService.CancelInvoice(invoice.Transaction);
        }

        public async Task ConfirmOrder(Invoice invoice)
        {
            var paypalOrder = await _paypalService.CaptureOrder(invoice.Transaction);
        }

        public async Task<Invoice> CreateInvoice(Domain.Entities.Order order)
        {
            List<PurchaseUnitRequest> purchaseUnits = new();
            purchaseUnits.Add(new()
            {
                CustomId = order.Id,
                Description = "Venta de Productos",
                AmountWithBreakdown = new AmountWithBreakdown()
                {
                    CurrencyCode = order.Currency.ToString(),

                    Value = Convert.ToInt32(order.TotalAmount).ToString()
                }
            });

            ApplicationContext applicationContext = new()
            {
                ReturnUrl = Path.Combine(_paymentProviderOptions.ReturnUrl, order.Id),
                CancelUrl = _paymentProviderOptions.CancelUrl
            };

            PaypalOrder payPalOrder = await _paypalService.CreatePaypalOrder(purchaseUnits, applicationContext);

            Uri uri = new(payPalOrder.Links.Single(l => l.Rel == "approve").Href);

            return new()
            {
                Link = uri,
                Transaction = payPalOrder.Id,
                PaymentProvider = PaymentProvider.PayPal,
                OrderId = order.Id
            };
        }

        public async Task<Invoice> UpdateInvoice(Invoice invoice, Domain.Entities.Order order)
        {
            ApplicationContext applicationContext = new()
            {
                ReturnUrl = Path.Combine(_paymentProviderOptions.ReturnUrl, order.Id),
                CancelUrl = _paymentProviderOptions.CancelUrl
            };

            PaypalOrder paypalOrder = await _paypalService.GetOrder(invoice.Transaction);


            PaypalOrder paypalOrderNew = await _paypalService.CreatePaypalOrder(paypalOrder.PurchaseUnits.Select(x => new PurchaseUnitRequest()
            {
                AmountWithBreakdown = x.AmountWithBreakdown,
                CustomId = x.CustomId,
                Description = x.Description
            }), applicationContext);

            await _paypalService.CancelInvoice(invoice.Transaction);

            Uri uri = new(paypalOrderNew.Links.Single(l => l.Rel == "approve").Href);

            invoice.Link = uri;
            invoice.Transaction = paypalOrderNew.Id;

            return invoice;
        }
    }
}
