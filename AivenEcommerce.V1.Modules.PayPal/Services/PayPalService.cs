﻿using AivenEcommerce.V1.Modules.PayPal.Enum;
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

        public async Task<PaypalOrder> CreatePaypalOrder(IEnumerable<PurchaseUnitRequest> purchaseUnits, Payer payer)
        {
            PayPalCheckoutSdk.Core.PayPalEnvironment environment = CreateEnvironment();
            var client = new PayPalHttpClient(environment);

            var payment = new OrderRequest()
            {
                CheckoutPaymentIntent = "CAPTURE",
                PurchaseUnits = purchaseUnits.ToList(),
                Payer = payer,
                ApplicationContext = CreateApplicationContext()
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

        public async Task<Uri> CreateUriForPayment(string customId, PayPalCurrency currency, string description, int totalAmount)
        {
            PayPalCheckoutSdk.Core.PayPalEnvironment environment = CreateEnvironment();
            var client = new PayPalHttpClient(environment);

            var payment = new OrderRequest()
            {
                CheckoutPaymentIntent = "CAPTURE",
                PurchaseUnits = new List<PurchaseUnitRequest>()
                {
                    new PurchaseUnitRequest()
                    {
                        CustomId = customId,
                        Description = description,
                        AmountWithBreakdown = new AmountWithBreakdown()
                        {
                            CurrencyCode = currency.ToString(),
                            Value = totalAmount.ToString()
                        }
                    }
                },
                ApplicationContext = CreateApplicationContext()
            };

            //https://developer.paypal.com/docs/api/orders/v2/#orders_create
            var request = new OrdersCreateRequest();
            request.Prefer("return=representation");
            request.RequestBody(payment);

            try
            {
                var response = await client.Execute(request);
                var result = response.Result<PaypalOrder>();
                var uri = new Uri(result.Links.Single(l => l.Rel == "approve").Href);

                return uri;
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
            if (_paypalOptions.Environment == Enum.PayPalEnvironment.Live)
            {
                return new LiveEnvironment(_paypalOptions.ClientId, _paypalOptions.ClientSecret);
            }
            else
            {
                return new SandboxEnvironment(_paypalOptions.ClientId, _paypalOptions.ClientSecret);
            }
        }

        public async Task<Uri> UpdateAmountInvoice(string paypalOrderId, int totalAmount)
        {
            PayPalCheckoutSdk.Core.PayPalEnvironment environment = CreateEnvironment();
            var client = new PayPalHttpClient(environment);

            var getOrderRequest = new OrdersGetRequest(paypalOrderId);

            try
            {
                var getOrderResponse = await client.Execute(getOrderRequest);
                var getOrderResult = getOrderResponse.Result<PaypalOrder>();
                var payment = new OrderRequest()
                {
                    CheckoutPaymentIntent = "CAPTURE",
                    PurchaseUnits = new List<PurchaseUnitRequest>()
                    {
                        new PurchaseUnitRequest()
                        {
                            CustomId = getOrderResult.PurchaseUnits[0].CustomId,
                            Description = getOrderResult.PurchaseUnits[0].Description,
                            AmountWithBreakdown = new AmountWithBreakdown()
                            {
                                CurrencyCode = getOrderResult.PurchaseUnits[0].AmountWithBreakdown.CurrencyCode,
                                Value = totalAmount.ToString()
                            }
                        }
                    },
                    ApplicationContext = CreateApplicationContext()
                };

                var request = new OrdersCreateRequest();
                request.Prefer("return=representation");
                request.RequestBody(payment);
                var response = await client.Execute(request);
                var result = response.Result<PaypalOrder>();
                var uri = new Uri(result.Links.Single(l => l.Rel == "approve").Href);

                await CancelInvoice(paypalOrderId);

                return uri;
            }
            catch (HttpException httpException)
            {
                var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();
                throw httpException;
            }
        }

        private ApplicationContext CreateApplicationContext()
        {

            var returnUrl = _paypalOptions.ReturnUrl;

            var cancelUrl = _paypalOptions.CancelUrl;

            return new ApplicationContext
            {
                ReturnUrl = returnUrl,
                CancelUrl = cancelUrl
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