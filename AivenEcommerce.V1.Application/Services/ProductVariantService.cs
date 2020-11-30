using AivenEcommerce.V1.Application.Mappers.ProductVariants;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Services;
using AivenEcommerce.V1.Domain.Shared.Dtos.ProductVariants;
using AivenEcommerce.V1.Domain.Shared.OperationResults;
using AivenEcommerce.V1.Domain.Validators;

using System;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<OperationResultEnumerable<ProductVariantDto>> UpdateAsync(UpdateProductVariantInput input)
        {
            var validationResult = await _productVariantValidator.ValidateUpdateProductVariant(input);

            if (validationResult.IsSuccess)
            {

                var taskGetVariants = _productVariantRepository.GetByProduct(new() { Id = input.ProductId });

                foreach (var variant in input.Variants)
                {
                    var entity = await _productVariantRepository.GetByNameAsync(new() { Id = input.ProductId }, variant.Name);

                    if (entity is null)
                    {
                        await _productVariantRepository.CreateAsync(new()
                        {
                            Name = variant.Name,
                            ProductId = input.ProductId,
                            Values = variant.Values
                        });
                    }
                    else
                    {
                        await _productVariantRepository.UpdateAsync(new()
                        {
                            Id = entity.Id,
                            Name = variant.Name,
                            ProductId = input.ProductId,
                            Values = variant.Values
                        });
                    }
                }


                var variants = await taskGetVariants;

                var variantsToDelete = variants.Where(x => !input.Variants.Select(y => y.Name).Contains(x.Name));

                foreach (var item in variantsToDelete)
                {
                    await _productVariantRepository.RemoveAsync(item);
                }

                variants = await _productVariantRepository.GetByProduct(new() { Id = input.ProductId });

                return OperationResultEnumerable<ProductVariantDto>.Success(variants.Select(x => x.ConvertToDto()));


            }

            return OperationResultEnumerable<ProductVariantDto>.Fail(validationResult);
        }
    }
}
