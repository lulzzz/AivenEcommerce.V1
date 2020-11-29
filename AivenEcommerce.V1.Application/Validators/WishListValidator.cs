using AivenEcommerce.V1.Application.Validations;
using AivenEcommerce.V1.Domain.Dtos.WishLists;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Validators;

using System;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Application.Validators
{
    public class WishListValidator : IWishListValidator
    {
        private readonly IWishListRepository _wishListRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;

        public WishListValidator(IWishListRepository wishListRepository, IProductRepository productRepository, ICustomerRepository customerRepository)
        {
            _wishListRepository = wishListRepository ?? throw new ArgumentNullException(nameof(wishListRepository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public async Task<ValidationResult> ValidateAddProductToWishList(AddProductToWishListInput input)
        {
            ValidationResult validationResult = new();

            if (string.IsNullOrWhiteSpace(input.CustomerEmail))
            {
                validationResult.Messages.Add(new(nameof(AddProductToWishListInput.CustomerEmail), "Debe indicar un cliente."));
            }
            else
            {
                Customer customer = await _customerRepository.GetCustomer(input.CustomerEmail);

                if (customer is null)
                {
                    validationResult.Messages.Add(new(nameof(AddProductToWishListInput.CustomerEmail), "No existe el cliente."));
                }
            }

            if (string.IsNullOrWhiteSpace(input.ProductId))
            {
                validationResult.Messages.Add(new(nameof(AddProductToWishListInput.ProductId), "Debe indicar un producto."));
            }
            else
            {
                Product product = await _productRepository.GetAsync(input.ProductId);

                if (product is null)
                {
                    validationResult.Messages.Add(new(nameof(AddProductToWishListInput.ProductId), "No existe el producto."));
                }
                else if (!product.IsActive)
                {
                    validationResult.Messages.Add(new(nameof(AddProductToWishListInput.ProductId), "El producto no esta activo."));
                }
            }

            return validationResult;
        }

        public async Task<ValidationResult> ValidateRemoveAllWishList(RemoveAllWishListInput input)
        {
            ValidationResult validationResult = new();

            if (string.IsNullOrWhiteSpace(input.CustomerEmail))
            {
                validationResult.Messages.Add(new(nameof(RemoveAllWishListInput.CustomerEmail), "Debe indicar un cliente."));
            }
            else
            {
                Customer customer = await _customerRepository.GetCustomer(input.CustomerEmail);

                if (customer is null)
                {
                    validationResult.Messages.Add(new(nameof(RemoveAllWishListInput.CustomerEmail), "No existe el cliente."));
                }
                else
                {
                    WishList wishList = await _wishListRepository.GetByCustomerAsync(input.CustomerEmail);

                    if (wishList is null)
                    {
                        validationResult.Messages.Add(new(nameof(RemoveAllWishListInput.CustomerEmail), "El cliente no posee una wishlist"));
                    }
                }
            }

            return validationResult;
        }

        public async Task<ValidationResult> ValidateRemoveProductToWishList(RemoveProductToWishListInput input)
        {
            ValidationResult validationResult = new();

            if (string.IsNullOrWhiteSpace(input.CustomerEmail))
            {
                validationResult.Messages.Add(new(nameof(RemoveProductToWishListInput.CustomerEmail), "Debe indicar un cliente."));
            }
            else
            {
                Customer customer = await _customerRepository.GetCustomer(input.CustomerEmail);

                if (customer is null)
                {
                    validationResult.Messages.Add(new(nameof(RemoveProductToWishListInput.CustomerEmail), "No existe el cliente."));
                }
                else
                {
                    WishList wishList = await _wishListRepository.GetByCustomerAsync(input.CustomerEmail);

                    if (wishList is null)
                    {
                        validationResult.Messages.Add(new(nameof(RemoveProductToWishListInput.CustomerEmail), "El cliente no posee una wishlist"));
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(input.ProductId))
            {
                validationResult.Messages.Add(new(nameof(RemoveProductToWishListInput.ProductId), "Debe indicar un producto."));
            }
            else
            {
                Product product = await _productRepository.GetAsync(input.ProductId);

                if (product is null)
                {
                    validationResult.Messages.Add(new(nameof(RemoveProductToWishListInput.ProductId), "No existe el producto."));
                }
            }

            return validationResult;
        }

        public async Task<ValidationResult> ValidateUpdateWishList(UpdateWishListInput input)
        {
            ValidationResult validationResult = new();

            if (string.IsNullOrWhiteSpace(input.CustomerEmail))
            {
                validationResult.Messages.Add(new(nameof(UpdateWishListInput.CustomerEmail), "Debe indicar un cliente."));
            }
            else
            {
                Customer customer = await _customerRepository.GetCustomer(input.CustomerEmail);

                if (customer is null)
                {
                    validationResult.Messages.Add(new(nameof(UpdateWishListInput.CustomerEmail), "No existe el cliente."));
                }
            }

            foreach (string productid in input.Products)
            {
                if (string.IsNullOrWhiteSpace(productid))
                {
                    validationResult.Messages.Add(new(nameof(UpdateWishListInput.Products), "Debe indicar un producto."));
                }
                else
                {
                    Product product = await _productRepository.GetAsync(productid);

                    if (product is null)
                    {
                        validationResult.Messages.Add(new(nameof(UpdateWishListInput.Products), "No existe el producto."));
                    }
                    else if (!product.IsActive)
                    {
                        validationResult.Messages.Add(new(nameof(UpdateWishListInput.Products), $"El producto {product.Name} no esta activo."));
                    }
                }
            }



            return validationResult;
        }
    }
}
