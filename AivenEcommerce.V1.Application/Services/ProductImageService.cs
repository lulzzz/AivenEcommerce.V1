using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AivenEcommerce.V1.Application.Mappers.ProductImages;
using AivenEcommerce.V1.Application.Mappers.Products;
using AivenEcommerce.V1.Domain.Dtos.ProductImages;
using AivenEcommerce.V1.Domain.Dtos.Products;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.OperationResults;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Services;
using AivenEcommerce.V1.Domain.Validators;

namespace AivenEcommerce.V1.Application.Services
{
    public class ProductImageService : IProductImageService
    {
        private readonly IImageUploaderService _imageService;
        private readonly IProductImageRepository _repository;
        private readonly IProductService _productService;
        private readonly IProductImageValidator _productImageValidator;

        public ProductImageService(IImageUploaderService imageService, IProductImageRepository repository, IProductService productService, IProductImageValidator productImageValidator)
        {
            _imageService = imageService ?? throw new ArgumentNullException(nameof(imageService));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _productImageValidator = productImageValidator ?? throw new ArgumentNullException(nameof(productImageValidator));
        }

        public async Task<OperationResult> DeleteImageAsync(DeleteProductImageInput input)
        {
            var validateResult = await _productImageValidator.ValidateDeleteProductImage(input);

            if (validateResult.IsSuccess)
            {
                var productDtoResult = await _productService.GetAsync(input.ProductId);

                if (!productDtoResult.IsSuccess)
                {
                    return OperationResultEnumerable<ProductImageDto>.Fail(productDtoResult.Validations);
                }

                var images = await _repository.GetProductImages(productDtoResult.Result.ConvertToEntity());

                images = images.Where(x => x.Id != input.ProductImageId);

                await _repository.UpdateProductImages(images);

                return OperationResult.Success();
            }
            else
            {
                return OperationResult.Fail(validateResult);
            }
        }

        public async Task<OperationResultEnumerable<ProductImageDto>> GetAllImageAsync(string productId)
        {
            var productDtoResult = await _productService.GetAsync(productId);

            if (!productDtoResult.IsSuccess)
            {
                return OperationResultEnumerable<ProductImageDto>.Fail(productDtoResult.Validations);
            }
            var images = await _repository.GetProductImages(productDtoResult.Result.ConvertToEntity());

            return OperationResultEnumerable<ProductImageDto>.Success(images.Select(x => x.ConvertToDto()));
        }

        public async Task<OperationResult<ProductImageDto>> UploadImageAsync(string productId, byte[] image)
        {
            var productDtoResult = await _productService.GetAsync(productId);

            if (!productDtoResult.IsSuccess)
            {
                return OperationResult<ProductImageDto>.Fail(productDtoResult.Validations);
            }

            Uri uriImage = await _imageService.UploadImage(image);

            ProductDto product = productDtoResult.Result;

            if (product?.Thumbnail is null)
            {
                productDtoResult = await _productService.UpdateAsync(
                    new (product.Id, product.Name, product.Cost, product.Price, product.PercentageOff, product.Category, product.SubCategory, uriImage)
                    );
            }

            IEnumerable<ProductImage> productImages = await _repository.GetProductImages(productDtoResult.Result.ConvertToEntity());
            List<ProductImage> newProductImages = new ();

            if (productImages is not null)
            {
                newProductImages.AddRange(productImages);
            }

            ProductImage productImage = new()
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                Image = uriImage
            };

            newProductImages.Add(productImage);

            if (productImages.Any())
            {
                await _repository.UpdateProductImages(newProductImages);
            }
            else
            {
                await _repository.CreateProductImages(newProductImages);
            }

            return OperationResult<ProductImageDto>.Success(productImage.ConvertToDto());

        }
    }
}
