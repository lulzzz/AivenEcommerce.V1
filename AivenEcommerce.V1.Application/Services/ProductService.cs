using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AivenEcommerce.V1.Application.Mappers.Products;
using AivenEcommerce.V1.Application.Validations;
using AivenEcommerce.V1.Domain.Dtos.Products;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Services;
using AivenEcommerce.V1.Domain.Validators;

using BusinessLogicEnterprise.Application.OperationResults;

namespace AivenEcommerce.V1.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IProductValidator _validator;

        public ProductService(IProductRepository repository, IProductValidator validator)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<OperationResult<ProductDto>> CreateAsync(CreateProductInput input)
        {
            ValidationResult validationResult = _validator.ValidateCreateProduct(input);

            if (validationResult.IsSuccess)
            {
                Product product = input.ConvertToEntity();

                product = await _repository.CreateAsync(product);

                return OperationResult<ProductDto>.Success(product.ConvertToDto());
            }
            else
            {
                return OperationResult<ProductDto>.Fail(validationResult);
            }

        }

        public async Task<OperationResult> DeleteAsync(DeleteProductInput input)
        {
            ValidationResult validationResult = await _validator.ValidateDeleteProduct(input);

            if (validationResult.IsSuccess)
            {
                await _repository.RemoveAsync(input.Id);

                return OperationResult.Success();
            }
            else
            {
                return OperationResult.Fail(validationResult);
            }

        }

        public async Task<OperationResult<ProductDto>> GetAsync(string id)
        {
            Product? product = await _repository.GetAsync(id);

            if (product is null)
            {
                return OperationResult<ProductDto>.NotFound();
            }

            return OperationResult<ProductDto>.Success(product.ConvertToDto());
        }

        public async Task<OperationResultEnumerable<ProductDto>> GetAllAsync()
        {
            IEnumerable<Product> products = await _repository.GetAllAsync();

            return OperationResultEnumerable<ProductDto>.Success(products.Select(x => x.ConvertToDto()));
        }

        public async Task<OperationResult<ProductDto>> UpdateAsync(UpdateProductInput input)
        {
            ValidationResult validationResult = await _validator.ValidateUpdateProduct(input);

            if (validationResult.IsSuccess)
            {
                Product product = input.ConvertToEntity();

                await _repository.UpdateAsync(product);

                return OperationResult<ProductDto>.Success(product.ConvertToDto());
            }
            else
            {
                return OperationResult<ProductDto>.Fail(validationResult);
            }
        }

        public async Task<OperationResult<ProductDto>> UpdateMainImageAsync(UpdateProductMainImageInput input)
        {
            Product? product = await _repository.GetAsync(input.ProductId);

            if (product is null)
            {
                return OperationResult<ProductDto>.NotFound();
            }

            product.Thumbnail = input.Image;

            await _repository.UpdateAsync(product);

            return OperationResult<ProductDto>.Success(product.ConvertToDto());
        }

        public async Task<OperationResult<ProductDto>> UpdateProductCostPriceAsync(UpdateProductCostPriceInput input)
        {
            ValidationResult validationResult = await _validator.ValidateUpdateProductCostPrice(input);

            if (validationResult.IsSuccess)
            {
                Product? product = await _repository.GetAsync(input.ProductId);

                product.Cost = input.Cost;
                product.Price = input.Price;

                await _repository.UpdateAsync(product);

                return OperationResult<ProductDto>.Success(product.ConvertToDto());
            }
            else
            {
                return OperationResult<ProductDto>.Fail(validationResult);
            }
        }

        public async Task<OperationResult<ProductDto>> UpdateProductCategoryAsync(UpdateProductCategorySubCategoryInput input)
        {
            ValidationResult validationResult = await _validator.ValidateUpdateProductCategory(input);

            if (validationResult.IsSuccess)
            {
                Product? product = await _repository.GetAsync(input.ProductId);

                product.Category = input.Category;
                product.SubCategory = input.SubCategory;

                await _repository.UpdateAsync(product);

                return OperationResult<ProductDto>.Success(product.ConvertToDto());
            }
            else
            {
                return OperationResult<ProductDto>.Fail(validationResult);
            }
        }
    }
}
