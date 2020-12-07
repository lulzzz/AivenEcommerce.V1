using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Shared.Dtos.Orders;
using AivenEcommerce.V1.Domain.Shared.Dtos.Products;
using AivenEcommerce.V1.Domain.Shared.Dtos.ProductVariants;
using AivenEcommerce.V1.Domain.Shared.OperationResults.Validations;
using AivenEcommerce.V1.Domain.Validators;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Application.Validators
{
    public class SaleValidator : ISaleValidator
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICouponCodeRepository _couponCodeRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly IAddressRepository _addressRepository;

        public SaleValidator(ICustomerRepository customerRepository, ICouponCodeRepository couponCodeRepository, IProductRepository productRepository, IProductVariantRepository productVariantRepository, IAddressRepository addressRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _couponCodeRepository = couponCodeRepository ?? throw new ArgumentNullException(nameof(couponCodeRepository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _productVariantRepository = productVariantRepository ?? throw new ArgumentNullException(nameof(productVariantRepository));
            _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
        }

        public async Task<ValidationResult> CreateSaleAsync(CreateSaleInput input)
        {
            ValidationResult validationResult = new();

            if (!Enum.IsDefined(input.PaymentProvider))
            {
                validationResult.Messages.Add(new(nameof(CreateSaleInput.PaymentProvider), "El proveedor de pago no es valido."));
            }

            if (string.IsNullOrWhiteSpace(input.CustomerEmail))
            {
                validationResult.Messages.Add(new(nameof(CreateSaleInput.CustomerEmail), "Debe indicar un cliente."));
            }
            else
            {
                Customer customer = await _customerRepository.GetCustomer(input.CustomerEmail);

                if (customer is null)
                {
                    validationResult.Messages.Add(new(nameof(CreateSaleInput.CustomerEmail), "No existe el cliente."));
                }
                else
                {
                    AddressCustomer addressCustomer = await _addressRepository.GetByCustomerAsync(input.CustomerEmail);
                    Address address = addressCustomer.Addresses?.SingleOrDefault(x => x.Id == input.AddressId);

                    if (address is null)
                    {
                        validationResult.Messages.Add(new(nameof(CreateSaleInput.AddressId), "La dirección no existe."));
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(input.CouponCode))
            {
                CouponCode couponCode = await _couponCodeRepository.GetCouponAsync(input.CouponCode);

                if (couponCode is null)
                {
                    validationResult.Messages.Add(new(nameof(CreateSaleInput.CouponCode), "El código de cupón no existe."));
                }
            }

            if (input.Products is null)
            {
                validationResult.Messages.Add(new(nameof(CreateSaleInput.Products), "Debe seleccionar productos."));
            }
            else
            {
                foreach (ProductDefinitive productDefinitive in input.Products)
                {
                    Product product = await _productRepository.GetAsync(productDefinitive.ProductId);

                    if (product is null)
                    {
                        validationResult.Messages.Add(new(nameof(CreateSaleInput.Products), $"El producto con Id {productDefinitive.ProductId} no existe."));
                    }
                    else if (!product.IsActive)
                    {
                        validationResult.Messages.Add(new(nameof(CreateSaleInput.Products), $"El producto {product.Name} no esta activo."));
                    }
                    else if(product.Stock <= 0)
                    {
                        validationResult.Messages.Add(new(nameof(CreateSaleInput.Products), $"El producto {product.Name} no tiene stock."));
                    }
                    else
                    {
                        IEnumerable<ProductVariant> variants = await _productVariantRepository.GetByProduct(product);

                        if (productDefinitive.Variants is not null && variants is not null)
                        {
                            foreach (ProductVariantPair variantPair in productDefinitive.Variants)
                            {
                                ProductVariant productVariant = variants.SingleOrDefault(x => x.Name == variantPair.Name);

                                if (productVariant is null)
                                {
                                    validationResult.Messages.Add(new(nameof(CreateSaleInput.Products), $"El producto {product.Name} no tiene variaciones de {variantPair.Name}."));
                                }
                                else if (!productVariant.Values.Contains(variantPair.Value))
                                {
                                    validationResult.Messages.Add(new(nameof(CreateSaleInput.Products), $"El producto {product.Name} no tiene una variación de {variantPair.Name} con el valor {variantPair.Value}."));
                                }
                            }
                        }
                    }
                }
            }



            return validationResult;
        }
    }
}
