using AivenEcommerce.V1.Application.Extensions;
using AivenEcommerce.V1.Application.Mappers.Baskets;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Services;
using AivenEcommerce.V1.Domain.Shared.Dtos.Baskets;
using AivenEcommerce.V1.Domain.Shared.Dtos.Products;
using AivenEcommerce.V1.Domain.Shared.OperationResults;
using AivenEcommerce.V1.Domain.Validators;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Application.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _repository;
        private readonly IProductRepository _productRepository;
        private readonly IBasketValidator _validator;

        public BasketService(IBasketRepository repository, IProductRepository productRepository, IBasketValidator validator)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<OperationResult<BasketDto>> AddBasketProductAsync(AddBasketProductInput input)
        {
            var validatioResult = await _validator.ValidateAddBasketProduct(input);
            if (validatioResult.IsSuccess)
            {
                var basket = await _repository.GetByCustomerAsync(input.CustomerEmail);

                if (basket is null)
                {
                    basket = await CreateEmplyBasket(input.CustomerEmail);
                }

                basket.Products = basket.Products.Add(input.Product);

                await _repository.UpdateAsync(basket);

                return OperationResult<BasketDto>.Success(basket.ConvertToDto());
            }

            return OperationResult<BasketDto>.Fail(validatioResult);
        }

        public async Task<OperationResult<BasketDto>> GetBasketAsync(string customerEmail)
        {
            var basket = await _repository.GetByCustomerAsync(customerEmail);

            return basket is null ? OperationResult<BasketDto>.NotFound()
                : OperationResult<BasketDto>.Success(basket.ConvertToDto());
        }

        public async Task<OperationResult<BasketProductsDto>> GetBasketProductsAsync(string customerEmail)
        {
            var basket = await _repository.GetByCustomerAsync(customerEmail);

            if (basket is null)
            {
                return OperationResult<BasketProductsDto>.NotFound();
            }

            var products = _productRepository.GetProducts(basket.Products.Select(x => x.ProductId));

            return OperationResult<BasketProductsDto>.Success(basket.ConvertToDto(products));
        }

        public async Task<OperationResult> RemoveAllBasketAsync(RemoveAllBasketInput input)
        {
            var validatioResult = await _validator.ValidateRemoveAllBasket(input);
            if (validatioResult.IsSuccess)
            {
                var basket = await _repository.GetByCustomerAsync(input.CustomerEmail);
                basket.Products = Enumerable.Empty<ProductDefinitive>();

                await _repository.UpdateAsync(basket);

                return OperationResult<BasketDto>.Success(basket.ConvertToDto());
            }

            return OperationResult<BasketDto>.Fail(validatioResult);
        }

        public async Task<OperationResult<BasketDto>> RemoveBasketProductAsync(RemoveBasketProductInput input)
        {
            var validatioResult = await _validator.ValidateRemoveBasketProduct(input);
            if (validatioResult.IsSuccess)
            {
                var basket = await _repository.GetByCustomerAsync(input.CustomerEmail);
                basket.Products = basket.Products.Where(x => x.ProductId != input.ProductId);

                await _repository.UpdateAsync(basket);

                return OperationResult<BasketDto>.Success(basket.ConvertToDto());
            }

            return OperationResult<BasketDto>.Fail(validatioResult);
        }

        public async Task<OperationResult<BasketDto>> UpdateBasketAsync(UpdateBasketInput input)
        {
            var validatioResult = await _validator.ValidateUpdateBasket(input);
            if (validatioResult.IsSuccess)
            {
                var basket = await _repository.GetByCustomerAsync(input.CustomerEmail);

                if (basket is null)
                {
                    basket = await CreateEmplyBasket(input.CustomerEmail);
                }

                basket.Products = input.Products;

                await _repository.UpdateAsync(basket);

                return OperationResult<BasketDto>.Success(basket.ConvertToDto());
            }

            return OperationResult<BasketDto>.Fail(validatioResult);
        }

        private Task<Basket> CreateEmplyBasket(string customerEmail) =>
            _repository.CreateAsync(new()
            {
                CustomerEmail = customerEmail,
                Products = Enumerable.Empty<ProductDefinitive>()
            });


    }
}
