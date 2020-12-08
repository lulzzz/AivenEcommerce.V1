using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Factories.PaymentProviders;
using AivenEcommerce.V1.Domain.Shared.Dtos.Products;
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

        public async Task<Invoice> CreateInvoice(Domain.Entities.Order order, SaleDetail saleDetail, IEnumerable<Product> products, Customer customer, Address address)
        {
            List<Item> items = new();

            foreach (ProductDefinitive productDefinitive in saleDetail.Products)
            {
                Product product = products.Single(x => x.Id == productDefinitive.ProductId);

                Item item = new()
                {
                    UnitAmount = new Money
                    {
                        CurrencyCode = order.Currency.ToString(),
                        Value = product.Price.ToString()
                    },
                    Description = product.Name,
                    Category = product.Category,
                    Quantity = productDefinitive.Quantity.ToString(),
                    Name = product.Name,
                    Sku = product.Id
                };

                items.Add(item);
            }


            List<PurchaseUnitRequest> purchaseUnits = new();
            purchaseUnits.Add(new()
            {
                CustomId = order.Id,
                Description = "Venta de Productos",
                AmountWithBreakdown = new AmountWithBreakdown()
                {
                    CurrencyCode = order.Currency.ToString(),

                    Value = order.TotalAmount.ToString()
                },
                Items = items

            });

            Payer payer = new()
            {
                Email = customer.Email,
                Name = new()
                {
                    FullName = customer.Name + " " + customer.LastName
                },
                PayerId = customer.Id.ToString(),
                AddressPortable = new()
                {
                    AddressDetails = new()
                    {
                        StreetName = address.Street,
                        StreetNumber = address.Number?.ToString()
                    },
                    AddressLine1 = address.Phone,
                    PostalCode = address.ZipCode
                }
            };


            ApplicationContext applicationContext = new()
            {
                ReturnUrl = Path.Combine(_paymentProviderOptions.ReturnUrl, order.Id),
                CancelUrl = _paymentProviderOptions.CancelUrl
            };

            PaypalOrder payPalOrder = await _paypalService.CreatePaypalOrder(purchaseUnits, payer, applicationContext);

            Uri uri = new(payPalOrder.Links.Single(l => l.Rel == "approve").Href);

            return new()
            {
                Link = uri,
                Transaction = payPalOrder.Id,
                PaymentProvider = PaymentProvider.PayPal,
                OrderId = order.Id
            };
        }

        public async Task<Invoice> UpdateInvoice(Invoice invoice, Domain.Entities.Order order, SaleDetail saleDetail, IEnumerable<Product> products, Customer customer, Address address)
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
                Items = x.Items,
                CustomId = x.CustomId,
                Description = x.Description
            }), paypalOrder.Payer, applicationContext);

            await _paypalService.CancelInvoice(invoice.Transaction);

            Uri uri = new(paypalOrderNew.Links.Single(l => l.Rel == "approve").Href);

            invoice.Link = uri;
            invoice.Transaction = paypalOrderNew.Id;

            return invoice;
        }
    }
}
