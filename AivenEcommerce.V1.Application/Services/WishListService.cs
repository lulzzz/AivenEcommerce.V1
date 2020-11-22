using System;
using System.Linq;
using System.Threading.Tasks;

using AivenEcommerce.V1.Application.Extensions;
using AivenEcommerce.V1.Application.Mappers.WishLists;
using AivenEcommerce.V1.Domain.Dtos.WishLists;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.OperationResults;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Services;
using AivenEcommerce.V1.Domain.Validators;

namespace AivenEcommerce.V1.Application.Services
{
    public class WishListService : IWishListService
    {
        private readonly IWishListRepository _repository;
        private readonly IProductRepository _productRepository;
        private readonly IWishListValidator _validator;

        public WishListService(IWishListRepository repository, IProductRepository productRepository, IWishListValidator validator)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<OperationResult<WishListDto>> AddProductToWishListAsync(AddProductToWishListInput input)
        {
            var validatioResult = await _validator.ValidateAddProductToWishList(input);

            if (validatioResult.IsSuccess)
            {
                var wishlist = await _repository.GetByCustomerAsync(input.CustomerEmail);

                if (wishlist is null)
                {
                    wishlist = await CreateEmplyBasket(input.CustomerEmail);
                }

                wishlist.Products = wishlist.Products.Add(input.ProductId);

                await _repository.UpdateAsync(wishlist);

                return OperationResult<WishListDto>.Success(wishlist.ConvertToDto());
            }

            return OperationResult<WishListDto>.Fail(validatioResult);
        }

        public async Task<OperationResult<WishListDto>> GetWishListAsync(string customerEmail)
        {
            var wishlist = await _repository.GetByCustomerAsync(customerEmail);

            return wishlist is null ? OperationResult<WishListDto>.NotFound()
                : OperationResult<WishListDto>.Success(wishlist.ConvertToDto());
        }

        public async Task<OperationResult<WishListProductsDto>> GetWishListWithProductInfoAsync(string customerEmail)
        {
            var wishlist = await _repository.GetByCustomerAsync(customerEmail);

            if (wishlist is null)
            {
                return OperationResult<WishListProductsDto>.NotFound();
            }

            var products = _productRepository.GetProducts(wishlist.Products);

            return OperationResult<WishListProductsDto>.Success(wishlist.ConvertToDto(products));
        }

        public async Task<OperationResult<WishListDto>> RemoveAllWishListAsync(RemoveAllWishListInput input)
        {
            var validatioResult = await _validator.ValidateRemoveAllWishList(input);

            if (validatioResult.IsSuccess)
            {
                var wishlist = await _repository.GetByCustomerAsync(input.CustomerEmail);
                wishlist.Products = Enumerable.Empty<string>();

                await _repository.UpdateAsync(wishlist);

                return OperationResult<WishListDto>.Success(wishlist.ConvertToDto());
            }

            return OperationResult<WishListDto>.Fail(validatioResult);
        }

        public async Task<OperationResult<WishListDto>> RemoveProductToWishListAsync(RemoveProductToWishListInput input)
        {
            var validatioResult = await _validator.ValidateRemoveProductToWishList(input);

            if (validatioResult.IsSuccess)
            {
                var wishlist = await _repository.GetByCustomerAsync(input.CustomerEmail);
                wishlist.Products = wishlist.Products.Where(x => x != input.ProductId);

                await _repository.UpdateAsync(wishlist);

                return OperationResult<WishListDto>.Success(wishlist.ConvertToDto());
            }

            return OperationResult<WishListDto>.Fail(validatioResult);
        }

        public async Task<OperationResult<WishListDto>> UpdateWishListAsync(UpdateWishListInput input)
        {
            var validatioResult = await _validator.ValidateUpdateWishList(input);

            if (validatioResult.IsSuccess)
            {
                var wishlist = await _repository.GetByCustomerAsync(input.CustomerEmail);

                if (wishlist is null)
                {
                    wishlist = await CreateEmplyBasket(input.CustomerEmail);
                }

                wishlist.Products = input.Products;

                await _repository.UpdateAsync(wishlist);

                return OperationResult<WishListDto>.Success(wishlist.ConvertToDto());
            }

            return OperationResult<WishListDto>.Fail(validatioResult);
        }

        private Task<WishList> CreateEmplyBasket(string customerEmail) =>
            _repository.CreateAsync(new()
            {
                CustomerEmail = customerEmail,
                Products = Enumerable.Empty<string>()
            });
    }
}
