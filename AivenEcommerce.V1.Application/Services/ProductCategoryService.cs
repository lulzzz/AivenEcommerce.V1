using System;
using System.Linq;
using System.Threading.Tasks;

using AivenEcommerce.V1.Application.Mappers.ProductCategories;
using AivenEcommerce.V1.Domain.Dtos.ProductCategories;
using AivenEcommerce.V1.Domain.OperationResults;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Services;
using AivenEcommerce.V1.Domain.Validators;

namespace AivenEcommerce.V1.Application.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProductCategoryValidator _productCategoryValidator;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository, IProductCategoryValidator productCategoryValidator)
        {
            _productCategoryRepository = productCategoryRepository ?? throw new ArgumentNullException(nameof(productCategoryRepository));
            _productCategoryValidator = productCategoryValidator ?? throw new ArgumentNullException(nameof(productCategoryValidator));
        }

        public async Task<OperationResult<ProductCategoryDto>> CreateAsync(CreateProductCategoryInput input)
        {
            var validationResult = await _productCategoryValidator.ValidateCreateProductCategory(input);
            if (validationResult.IsSuccess)
            {
                var entity = input.ConvertToEntity();

                entity = await _productCategoryRepository.CreateAsync(entity);

                return OperationResult<ProductCategoryDto>.Success(entity.ConvertToDto());
            }

            return OperationResult<ProductCategoryDto>.Fail(validationResult);
        }

        public async Task<OperationResult> DeleteAsync(DeleteProductCategoryInput input)
        {
            var validationResult = await _productCategoryValidator.ValidateDeleteProductCategory(input);

            if (validationResult.IsSuccess)
            {
                var entity = await _productCategoryRepository.GetByNameAsync(input.Name);

                await _productCategoryRepository.RemoveAsync(entity);

                return OperationResult.Success();
            }

            return OperationResult.Fail(validationResult);

        }

        public async Task<OperationResultEnumerable<ProductCategoryDto>> GetAllAsync()
        {
            var categories = await _productCategoryRepository.GetAllAsync();

            return OperationResultEnumerable<ProductCategoryDto>.Success(categories.Select(x => x.ConvertToDto()));
        }

        public async Task<OperationResult<ProductCategoryDto>> GetAsync(string name)
        {
            var entity = await _productCategoryRepository.GetByNameAsync(name);

            if (entity is null)
            {
                return OperationResult<ProductCategoryDto>.NotFound();
            }

            return OperationResult<ProductCategoryDto>.Success(entity.ConvertToDto());
        }

        public async Task<OperationResult<ProductCategoryDto>> UpdateAsync(UpdateProductCategoryInput input)
        {
            var validationResult = await _productCategoryValidator.ValidateUpdateProductCategory(input);

            if (validationResult.IsSuccess)
            {
                var entity = await _productCategoryRepository.GetAsync(input.Id);

                await _productCategoryRepository.UpdateAsync(entity);

                return OperationResult<ProductCategoryDto>.Success(entity.ConvertToDto());
            }

            return OperationResult<ProductCategoryDto>.Fail(validationResult);
        }
    }
}
