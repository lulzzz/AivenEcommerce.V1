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
        private readonly ICouponCodeRepository _couponCodeRepository;
        private readonly IPaymentProviderFactory _paymentProviderFactory;

        public SaleService(ISaleValidator saleValidator, ISaleRepository saleRepository, ICustomerRepository customerRepository, IAddressRepository addressRepository, IOrderRepository orderRepository, ISaleDetailRepository saleDetailRepository, IInvoiceRepository invoiceRepository, IProductRepository productRepository, IDeliveryRepository deliveryRepository, ICouponCodeRepository couponCodeRepository, IPaymentProviderFactory paymentProviderFactory)
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
            _couponCodeRepository = couponCodeRepository ?? throw new ArgumentNullException(nameof(couponCodeRepository));
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

                CouponCode couponCode = null;

                if (!string.IsNullOrWhiteSpace(input.CouponCode))
                {
                    couponCode = await _couponCodeRepository.GetCouponAsync(input.CouponCode);
                }

                Order order = new()
                {
                    Status = OrderStatus.Created,
                    CreationDate = DateTime.Now,
                    CustomerEmail = input.CustomerEmail,
                    Type = OrderType.ProductSale,
                    TotalAmount = CalculateTotalAmount(couponCode, products, customer)
                };

                order = await _orderRepository.CreateAsync(order);

                Sale sale = new()
                {
                    CouponCode = input.CouponCode,
                    OrderId = order.Id,
                    Products = products.Select(x => x.Id),
                    Status = SaleStatus.PendingPayment
                };

                sale = await _saleRepository.CreateAsync(sale);

                SaleDetail saleDetail = new()
                {
                    SaleId = sale.Id,
                    Products = input.Products
                };

                saleDetail = await _saleDetailRepository.CreateAsync(saleDetail);

                IPaymentProvider paymentProvider = _paymentProviderFactory.CreatePaymentProvider(input.PaymentProvider);

                Invoice invoice = await paymentProvider.CreateInvoice(order);

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

        private static decimal CalculateTotalAmount(CouponCode couponCode, IEnumerable<Product> products, Customer customer)
        {
            decimal totalAmount = products.Sum(x => x.Price);

            if (couponCode is null)
                return totalAmount;

            if (totalAmount < couponCode.MinAmount || totalAmount > couponCode.MaxAmount)
                return totalAmount;

            if (DateTime.Today < couponCode.DateStart || DateTime.Today > couponCode.DateExpire)
                return totalAmount;

            List<Product> productsToOff = new();


            if (couponCode.Customers?.Contains(customer.Email) ?? false)
            {
                productsToOff.AddRange(products);
            }
            else
            {
                if (couponCode.Categories?.Any() ?? false)
                    productsToOff.AddRange(products.Where(x => couponCode.Categories.Contains(x.Category)));

                if (couponCode.SubCategories?.Any() ?? false)
                    productsToOff.AddRange(products.Where(x => couponCode.SubCategories.Any(y => y.Category == x.Category && y.SubCategory == x.SubCategory)));

                if (couponCode.Products?.Any() ?? false)
                    productsToOff.AddRange(products.Where(x => couponCode.Products.Contains(x.Id)));
            }

            decimal valueOff = couponCode.Type switch
            {
                CouponCodeOffType.Percentage => (couponCode.Value / productsToOff.Sum(x => x.Price)) * 100,
                CouponCodeOffType.SpecificAmount => productsToOff.Sum(x => x.Price) - couponCode.Value,
                _ => throw new ArgumentException("CouponCodeOffType invalido.", nameof(couponCode))
            };


            return totalAmount - valueOff;

        }
    }
}
