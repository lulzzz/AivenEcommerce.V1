using System;
using System.Collections.Generic;
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
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryValidator _productCategoryValidator;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository, IProductRepository productRepository, IProductCategoryValidator productCategoryValidator)
        {
            _productCategoryRepository = productCategoryRepository ?? throw new ArgumentNullException(nameof(productCategoryRepository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _productCategoryValidator = productCategoryValidator ?? throw new ArgumentNullException(nameof(productCategoryValidator));
        }

        public async Task<OperationResult<ProductCategoryDto>> CreateAsync(CreateProductCategoryInput input)
        {
            var validationResult = await _productCategoryValidator.ValidateCreateProductCategory(input);
            if (validationResult.IsSuccess)
            {
                var entity = input.ConvertToEntity();

                entity = await _productCategoryRepository.CreateAsync(entity);

                return OperationResult<ProductCategoryDto>.Success(entity.ConvertToDto(0));
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

                await _productRepository.InactiveProductsByCategory(entity.Name);

                return OperationResult.Success();
            }

            return OperationResult.Fail(validationResult);

        }

        public async Task<OperationResult> DeleteSubCategoryAsync(DeleteProductSubCategoryInput input)
        {
            var validationResult = await _productCategoryValidator.ValidateDeleteProductSubCategory(input);

            if (validationResult.IsSuccess)
            {
                var entity = await _productCategoryRepository.GetByNameAsync(input.CategoryName);

                entity.SubCategories = entity.SubCategories.Where(x => x != input.SubCategoryName).ToList();

                await _productCategoryRepository.UpdateAsync(entity);

                await _productRepository.InactiveProductsByCategory(input.CategoryName, input.SubCategoryName);

                return OperationResult.Success();
            }

            return OperationResult.Fail(validationResult);

        }

        public async Task<OperationResultEnumerable<ProductCategoryDto>> GetAllAsync()
        {
            var categories = await _productCategoryRepository.GetAllAsync();

            List<ProductCategoryDto> categoryDtos = new();

            foreach (var item in categories)
            {
                var productCount = await _productRepository.CountByCategory(item.Name);
                categoryDtos.Add(item.ConvertToDto(productCount));
            }

            return OperationResultEnumerable<ProductCategoryDto>.Success(categoryDtos);
        }

        public async Task<OperationResult<ProductCategoryDto>> GetAsync(string name)
        {
            var entity = await _productCategoryRepository.GetByNameAsync(name);

            if (entity is null)
            {
                return OperationResult<ProductCategoryDto>.NotFound();
            }

            var productCount = await _productRepository.CountByCategory(name);

            return OperationResult<ProductCategoryDto>.Success(entity.ConvertToDto(productCount));
        }

        public async Task<OperationResultEnumerable<ProductSubCategoryDto>> GetSubCategories(string categoryName)
        {
            var entity = await _productCategoryRepository.GetByNameAsync(categoryName);

            List<ProductSubCategoryDto> subcategories = new();

            foreach (var item in entity.SubCategories)
            {
                var productCount = await _productRepository.CountBySubCategory(categoryName, item);

                subcategories.Add(new(categoryName, item, productCount));
            }

            return OperationResultEnumerable<ProductSubCategoryDto>.Success(subcategories);
        }

        public async Task<OperationResult<ProductCategoryDto>> UpdateAsync(UpdateProductCategoryInput input)
        {
            var validationResult = await _productCategoryValidator.ValidateUpdateProductCategory(input);

            if (validationResult.IsSuccess)
            {
                var entity = await _productCategoryRepository.GetByNameAsync(input.OldName);
                await _productCategoryRepository.RemoveAsync(entity);

                entity.Name = input.NewName;
                entity.SubCategories = input.SubCategories;

                await _productCategoryRepository.CreateAsync(entity);

                var productCount = await _productRepository.CountByCategory(entity.Name);

                return OperationResult<ProductCategoryDto>.Success(entity.ConvertToDto(productCount));
            }

            return OperationResult<ProductCategoryDto>.Fail(validationResult);
        }

        public async Task<OperationResult<ProductCategoryDto>> UpdateCategoryNameAsync(UpdateProductCategoryNameInput input)
        {
            var validationResult = await _productCategoryValidator.ValidateUpdateProductCategoryNameCategory(input);

            if (validationResult.IsSuccess)
            {
                var entity = await _productCategoryRepository.GetByNameAsync(input.OldCategoryName);

                await _productCategoryRepository.RemoveAsync(entity);

                entity.Name = input.NewCategoryName;
                await _productCategoryRepository.CreateAsync(entity);

                await _productRepository.UpdateCategoryName(input.OldCategoryName, input.NewCategoryName);

                var productCount = await _productRepository.CountByCategory(entity.Name);

                return OperationResult<ProductCategoryDto>.Success(entity.ConvertToDto(productCount));
            }

            return OperationResult<ProductCategoryDto>.Fail(validationResult);
        }

        public async Task<OperationResult<ProductCategoryDto>> UpdateSubCategoryNameAsync(UpdateProductSubCategoryNameInput input)
        {
            var validationResult = await _productCategoryValidator.ValidateUpdateProductSubCategoryNameCategory(input);

            if (validationResult.IsSuccess)
            {
                var entity = await _productCategoryRepository.GetByNameAsync(input.CategoryName);

                var list = entity.SubCategories.Where(x => x != input.OldSubCategoryName).ToList();
                list.Add(input.NewSubCategoryName);

                entity.SubCategories = list;

                await _productCategoryRepository.UpdateAsync(entity);

                await _productRepository.UpdateSubCategoryName(input.OldSubCategoryName, input.NewSubCategoryName);

                var productCount = await _productRepository.CountByCategory(entity.Name);

                return OperationResult<ProductCategoryDto>.Success(entity.ConvertToDto(productCount));
            }

            return OperationResult<ProductCategoryDto>.Fail(validationResult);
        }
    }
}
