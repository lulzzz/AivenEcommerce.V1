using System;
using System.Linq;
using System.Threading.Tasks;

using AivenEcommerce.V1.Application.Validations;
using AivenEcommerce.V1.Domain.Dtos.ProductImages;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Validators;

namespace AivenEcommerce.V1.Application.Validators
{
    public class ProductImageValidator : IProductImageValidator
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductImageRepository _productImageRepository;

        public ProductImageValidator(IProductRepository productRepository, IProductImageRepository productImageRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _productImageRepository = productImageRepository ?? throw new ArgumentNullException(nameof(productImageRepository));
        }

        public async Task<ValidationResult> ValidateDeleteProductImage(DeleteProductImageInput input)
        {
            ValidationResult validationResult = new ();

            Product? product = await _productRepository.GetAsync(input.ProductId);

            if (product is null)
            {
                validationResult.Messages.Add(new (nameof(DeleteProductImageInput.ProductId), "El producto no existe."));
            }
            else
            {
                var images = await _productImageRepository.GetProductImages(product);

                var image = images.SingleOrDefault(x => x.Id == input.ProductImageId);

                if (image is null)
                {
                    validationResult.Messages.Add(new (nameof(DeleteProductImageInput.ProductImageId), "La imagen no existe."));
                }
                else if (product.Thumbnail == image.Image)
                {
                    validationResult.Messages.Add(new (nameof(DeleteProductImageInput.ProductImageId), "No se puede eliminar la imagen principal del producto."));
                }
            }

            return validationResult;
        }
    }
}
