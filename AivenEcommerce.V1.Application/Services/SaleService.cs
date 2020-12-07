using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Factories.PaymentProviders;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Services;
using AivenEcommerce.V1.Domain.Shared.Dtos.Orders;
using AivenEcommerce.V1.Domain.Shared.Dtos.Products;
using AivenEcommerce.V1.Domain.Shared.Dtos.Sales;
using AivenEcommerce.V1.Domain.Shared.Enums;
using AivenEcommerce.V1.Domain.Shared.OperationResults;
using AivenEcommerce.V1.Domain.Validators;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private readonly IProductRepository _productRepository;
        private readonly IPaymentProviderFactory _paymentProviderFactory;
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

                IPaymentProvider paymentProvider = _paymentProviderFactory.CreatePaymentProvider(PaymentProvider.PayPal);
                Invoice invoice = await paymentProvider.CreateInvoice(order, saleDetail, products, customer, address);
            }

            return OperationResult<SaleOrderDto>.Fail(validationResult);
        }
    }
}
