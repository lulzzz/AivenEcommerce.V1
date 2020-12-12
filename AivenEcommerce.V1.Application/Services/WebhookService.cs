using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Factories.PaymentProviders;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Services;
using AivenEcommerce.V1.Domain.Shared.Enums;
using AivenEcommerce.V1.Domain.Shared.OperationResults;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Application.Services
{
    public class WebhookService : IWebhookService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ISaleRepository _saleRepository;
        private readonly ISaleDetailRepository _saleDetailRepository;
        private readonly IProductRepository _productRepository;
        private readonly IPaymentProviderFactory _paymentProviderFactory;

        public WebhookService(IOrderRepository orderRepository, IDeliveryRepository deliveryRepository, IInvoiceRepository invoiceRepository, ISaleRepository saleRepository, ISaleDetailRepository saleDetailRepository, IProductRepository productRepository, IPaymentProviderFactory paymentProviderFactory)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _deliveryRepository = deliveryRepository ?? throw new ArgumentNullException(nameof(deliveryRepository));
            _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
            _saleRepository = saleRepository ?? throw new ArgumentNullException(nameof(saleRepository));
            _saleDetailRepository = saleDetailRepository ?? throw new ArgumentNullException(nameof(saleDetailRepository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _paymentProviderFactory = paymentProviderFactory ?? throw new ArgumentNullException(nameof(paymentProviderFactory));
        }

        public async Task<OperationResult> InvoiceWebhookPayPal(string orderId, string token)
        {
            Order order = await _orderRepository.GetAsync(orderId);

            Invoice invoice = await _invoiceRepository.GetInvoiceByOrderAsync(order);

            await _paymentProviderFactory.CreatePaymentProvider(PaymentProvider.PayPal).ConfirmOrder(invoice);

            Delivery delivery = await _deliveryRepository.GetDeliveryAsync(order);

            order.Status = OrderStatus.Payed;
            order.PayDate = DateTime.Now;

            await _orderRepository.UpdateAsync(order);

            delivery.Status = DeliveryStatus.Pending;

            await _deliveryRepository.UpdateAsync(delivery);

            if (order.Type == OrderType.ProductSale)
            {
                Sale sale = await _saleRepository.GetSaleAsync(order);
                sale.Status = SaleStatus.Payed;
                await _saleRepository.UpdateAsync(sale);

                SaleDetail saleDetail = await _saleDetailRepository.GetBySaleAsync(sale);
                IEnumerable<Product> products = _productRepository.GetAvailableProducts(sale.Products.Distinct());
                foreach (Product product in products)
                {
                    product.Stock -= saleDetail.Products.Where(x => x.ProductId == product.Id).Sum(x => x.Quantity);

                    if (product.Stock <= 0)
                    {
                        product.IsActive = false;
                    }

                    await _productRepository.UpdateAsync(product);
                }
            }


            return OperationResult.Success();
        }
    }
}
