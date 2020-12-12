using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Shared.Dtos.Baskets;
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
    public class BasketValidator : IBasketValidator
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly ICustomerRepository _customerRepository;

        public BasketValidator(IBasketRepository basketRepository, IProductRepository productRepository, IProductVariantRepository productVariantRepository, ICustomerRepository customerRepository)
        {
            _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _productVariantRepository = productVariantRepository ?? throw new ArgumentNullException(nameof(productVariantRepository));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public async Task<ValidationResult> ValidateAddBasketProductAsync(AddBasketProductInput input)
        {
            ValidationResult validationResult = new();

            if (string.IsNullOrWhiteSpace(input.CustomerEmail))
            {
                validationResult.Messages.Add(new(nameof(AddBasketProductInput.CustomerEmail), $"Debe ingresar un cliente."));
            }
            else
            {
                Customer customer = await _customerRepository.GetCustomer(input.CustomerEmail);

                if (customer is null)
                {
                    validationResult.Messages.Add(new(nameof(AddBasketProductInput.CustomerEmail), $"El usuario '{input.CustomerEmail}' no existe."));
                }
            }

            if (input.Product is null)
            {
                validationResult.Messages.Add(new(nameof(AddBasketProductInput.Product), $"Debe seleccionar un producto."));
            }
            else
            {
                Product product = await _productRepository.GetAsync(input.Product.ProductId);
                if (product is not null)
                {
                    if (!product.IsActive)
                    {
                        validationResult.Messages.Add(new(nameof(AddBasketProductInput.Product), $"El producto '{product.Name}' no esta activo."));
                    }

                    if (input.Product.Quantity <= 0)
                    {
                        validationResult.Messages.Add(new(nameof(AddBasketProductInput.Product.Quantity), $"La cantidad debe ser mayor a cero."));
                    }

                    IEnumerable<ProductVariant> variants = await _productVariantRepository.GetByProduct(product);

                    foreach (ProductVariantPair variantInput in input.Product.Variants)
                    {
                        if (!variants.Any(x => x.Name == variantInput.Name && x.Values.Contains(variantInput.Value)))
                        {
                            validationResult.Messages.Add(new(nameof(AddBasketProductInput.Product.Variants), $"La variante '{variantInput.Name}' con valor '{variantInput.Value}' del producto '{product.Name}' no existe."));
                        }
                    }
                }
                else
                {
                    validationResult.Messages.Add(new(nameof(AddBasketProductInput.Product.ProductId), "El producto no existe."));
                }
            }

            return validationResult;
        }

        public async Task<ValidationResult> ValidateRemoveAllBasketAsync(RemoveAllBasketInput input)
        {
            ValidationResult validationResult = new();

            if (string.IsNullOrWhiteSpace(input.CustomerEmail))
            {
                validationResult.Messages.Add(new(nameof(AddBasketProductInput.CustomerEmail), $"Debe ingresar un cliente."));
            }
            else
            {
                Customer customer = await _customerRepository.GetCustomer(input.CustomerEmail);

                if (customer is null)
                {
                    validationResult.Messages.Add(new(nameof(AddBasketProductInput.CustomerEmail), $"El usuario '{input.CustomerEmail}' no existe."));
                }
            }

            Basket basket = await _basketRepository.GetByCustomerAsync(input.CustomerEmail);

            if (basket is null)
            {
                validationResult.Messages.Add(new(nameof(RemoveAllBasketInput.CustomerEmail), $"El carrito no existe."));
            }

            return validationResult;
        }

        public async Task<ValidationResult> ValidateRemoveBasketProductAsync(RemoveBasketProductInput input)
        {
            ValidationResult validationResult = new();

            if (string.IsNullOrWhiteSpace(input.CustomerEmail))
            {
                validationResult.Messages.Add(new(nameof(AddBasketProductInput.CustomerEmail), $"Debe ingresar un cliente."));
            }
            else
            {
                Customer customer = await _customerRepository.GetCustomer(input.CustomerEmail);

                if (customer is null)
                {
                    validationResult.Messages.Add(new(nameof(AddBasketProductInput.CustomerEmail), $"El usuario '{input.CustomerEmail}' no existe."));
                }
            }

            Basket basket = await _basketRepository.GetByCustomerAsync(input.CustomerEmail);

            if (basket is null)
            {
                validationResult.Messages.Add(new(nameof(RemoveBasketProductInput.CustomerEmail), $"El carrito no existe."));
            }
            else if (!basket.Products.Any(x => x.ProductId == input.ProductId))
            {
                validationResult.Messages.Add(new(nameof(RemoveBasketProductInput.CustomerEmail), $"El producto no esta en el carrito."));
            }

            return validationResult;
        }

        public async Task<ValidationResult> ValidateUpdateBasketAsync(UpdateBasketInput input)
        {
            ValidationResult validationResult = new();

            if (string.IsNullOrWhiteSpace(input.CustomerEmail))
            {
                validationResult.Messages.Add(new(nameof(UpdateBasketInput.CustomerEmail), $"Debe ingresar un cliente."));
            }
            else
            {
                Customer customer = await _customerRepository.GetCustomer(input.CustomerEmail);

                if (customer is null)
                {
                    validationResult.Messages.Add(new(nameof(UpdateBasketInput.CustomerEmail), $"El cliente '{input.CustomerEmail}' no existe."));
                }

            }


            foreach (ProductDefinitive productInput in input.Products)
            {
                Product product = await _productRepository.GetAsync(productInput.ProductId);
                if (product is null)
                {
                    validationResult.Messages.Add(new(nameof(UpdateBasketInput.Products), "El producto no existe."));
                }
                else
                {
                    if (productInput.Quantity <= 0)
                    {
                        validationResult.Messages.Add(new(nameof(UpdateBasketInput.Products), $"La cantidad debe ser mayor a cero para el producto {product.Name}."));
                    }

                    if (!product.IsActive)
                    {
                        validationResult.Messages.Add(new(nameof(UpdateBasketInput.Products), $"El producto {product.Name} debe estar activo."));
                    }

                    IEnumerable<ProductVariant> variants = await _productVariantRepository.GetByProduct(product);

                    foreach (ProductVariantPair variantInput in productInput.Variants)
                    {
                        if (!variants.Any(x => x.Name == variantInput.Name && x.Values.Contains(variantInput.Value)))
                        {
                            validationResult.Messages.Add(new(nameof(UpdateBasketInput.Products), $"La variante '{variantInput.Name}' con valor '{variantInput.Value}' del producto '{product.Name}' no existe."));
                        }
                    }
                }
            }

            return validationResult;
        }
    }
}
