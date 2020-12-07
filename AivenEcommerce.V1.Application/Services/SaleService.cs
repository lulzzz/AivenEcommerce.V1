using AivenEcommerce.V1.Application.Mappers.Invoices;
using AivenEcommerce.V1.Application.Mappers.Orders;
using AivenEcommerce.V1.Application.Mappers.Sales;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Factories.PaymentProviders;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Services;
using AivenEcommerce.V1.Domain.Shared.Dtos.Orders;
using AivenEcommerce.V1.Domain.Shared.Dtos.Sales;
using AivenEcommerce.V1.Domain.Shared.Enums;
using AivenEcommerce.V1.Domain.Shared.OperationResults;
using AivenEcommerce.V1.Domain.Validators;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Application.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleValidator _saleValidator;
        private readonly ISaleRepository _saleRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ISaleDetailRepository _saleDetailRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IProductRepository _productRepository;
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly IPaymentProviderFactory _paymentProviderFactory;

        public SaleService(ISaleValidator saleValidator, ISaleRepository saleRepository, ICustomerRepository customerRepository, IAddressRepository addressRepository, IOrderRepository orderRepository, ISaleDetailRepository saleDetailRepository, IInvoiceRepository invoiceRepository, IProductRepository productRepository, IDeliveryRepository deliveryRepository, IPaymentProviderFactory paymentProviderFactory)
        {
            _saleValidator = saleValidator ?? throw new ArgumentNullException(nameof(saleValidator));
            _saleRepository = saleRepository ?? throw new ArgumentNullException(nameof(saleRepository));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _saleDetailRepository = saleDetailRepository ?? throw new ArgumentNullException(nameof(saleDetailRepository));
            _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _deliveryRepository = deliveryRepository ?? throw new ArgumentNullException(nameof(deliveryRepository));
            _paymentProviderFactory = paymentProviderFactory ?? throw new ArgumentNullException(nameof(paymentProviderFactory));
        }

        public async Task<OperationResult<SaleOrderDto>> CreateSaleAsync(CreateSaleInput input)
        {
            var validationResult = await _saleValidator.CreateSaleAsync(input);

            if (validationResult.IsSuccess)
            {
                IEnumerable<Product> products = _productRepository.GetAvailableProducts(input.Products.Select(x => x.ProductId));

                Customer customer = await _customerRepository.GetCustomer(input.CustomerEmail);
                AddressCustomer addressCustomer = await _addressRepository.GetByCustomerAsync(input.CustomerEmail);
                Address address = addressCustomer.Addresses.Single(x => x.Id == input.AddressId);

                Order order = new()
                {
                    Status = OrderStatus.Created,
                    CreationDate = DateTime.Now,
                    CustomerEmail = input.CustomerEmail,
                    Type = OrderType.ProductSale,
                    TotalAmount = products.Sum(x => x.Price)
                };

                order = await _orderRepository.CreateAsync(order);

                Sale sale = new()
                {
                    CouponCode = input.CouponCode,
                    OrderId = order.Id,
                    Products = products.Select(x => x.Id)
                };

                sale = await _saleRepository.CreateAsync(sale);

                SaleDetail saleDetail = new()
                {
                    SaleId = sale.Id,
                    Products = input.Products
                };

                saleDetail = await _saleDetailRepository.CreateAsync(saleDetail);

                IPaymentProvider paymentProvider = _paymentProviderFactory.CreatePaymentProvider(input.PaymentProvider);

                Invoice invoice = await paymentProvider.CreateInvoice(order, saleDetail, products, customer, address);

                invoice = await _invoiceRepository.CreateAsync(invoice);

                Delivery delivery = new()
                {
                    AddressId = input.AddressId,
                    OrderId = order.Id,
                    Status = DeliveryStatus.Created
                };

                delivery = await _deliveryRepository.CreateAsync(delivery);

                return OperationResult<SaleOrderDto>.Success(new(sale.ConvertToDto(), order.ConvertToDto(), invoice.ConvertToDto()));
            }

            return OperationResult<SaleOrderDto>.Fail(validationResult);
        }
    }
}
