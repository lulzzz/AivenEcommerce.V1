using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AivenEcommerce.V1.Application.Mappers.ProductVariants;
using AivenEcommerce.V1.Domain.Dtos.ProductVariants;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.OperationResults;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Services;
using AivenEcommerce.V1.Domain.Validators;

namespace AivenEcommerce.V1.Application.Services
{
    public class ProductVariantService : IProductVariantService
    {
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly IProductVariantValidator _productVariantValidator;

        public ProductVariantService(IProductVariantRepository productVariantRepository, IProductVariantValidator productVariantValidator)
        {
            _productVariantRepository = productVariantRepository ?? throw new ArgumentNullException(nameof(productVariantRepository));
            _productVariantValidator = productVariantValidator ?? throw new ArgumentNullException(nameof(productVariantValidator));
        }

        public async Task<OperationResult<ProductVariantDto>> CreateAsync(CreateProductVariantInput input)
        {
            var validationResult = await _productVariantValidator.ValidateCreateProductVariant(input);
            if (validationResult.IsSuccess)
            {
                var entity = input.ConvertToEntity();

                entity = await _productVariantRepository.CreateAsync(entity);

                return OperationResult<ProductVariantDto>.Success(entity.ConvertToDto());
            }

            return OperationResult<ProductVariantDto>.Fail(validationResult);
        }

        public async Task<OperationResult> DeleteAsync(DeleteProductVariantInput input)
        {
            var validationResult = await _productVariantValidator.ValidateDeleteProductVariant(input);

            if (validationResult.IsSuccess)
            {
                var entity = await _productVariantRepository.GetByNameAsync(new() { Id = input.ProductId }, input.Name);

                await _productVariantRepository.RemoveAsync(entity);

                return OperationResult.Success();
            }

            return OperationResult.Fail(validationResult);
        }

        public async Task<OperationResultEnumerable<ProductVariantDto>> GetAllAsync(string productId)
        {
            var entity = await _productVariantRepository.GetByProduct(new() { Id = productId });

            return OperationResultEnumerable<ProductVariantDto>.Success(entity.Select(x => x.ConvertToDto()));
        }

        public async Task<OperationResult<ProductVariantDto>> GetAsync(string productId, string name)
        {
            var entity = await _productVariantRepository.GetByNameAsync(new() { Id = productId }, name);

            if (entity is null)
            {
                return OperationResult<ProductVariantDto>.NotFound();
            }

            return OperationResult<ProductVariantDto>.Success(entity.ConvertToDto());
        }

        public async Task<OperationResult<ProductVariantDto>> UpdateAsync(UpdateProductVariantInput input)
        {
            var validationResult = await _productVariantValidator.ValidateUpdateProductVariant(input);
            if (validationResult.IsSuccess)
            {
                var entity = await _productVariantRepository.GetByNameAsync(new() { Id = input.ProductId }, input.Name);

                entity.Values = input.Values;

                await _productVariantRepository.UpdateAsync(entity);

                return OperationResult<ProductVariantDto>.Success(entity.ConvertToDto());
            }

            return OperationResult<ProductVariantDto>.Fail(validationResult);
        }
    }
}
