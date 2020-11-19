using System;
using System.Linq;
using System.Threading.Tasks;

using AivenEcommerce.V1.Application.Extensions;
using AivenEcommerce.V1.Application.Validations;
using AivenEcommerce.V1.Domain.Dtos.ProductVariants;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Validators;

namespace AivenEcommerce.V1.Application.Validators
{
    public class ProductVariantValidator : IProductVariantValidator
    {
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly IProductRepository _productRepository;

        public ProductVariantValidator(IProductVariantRepository productVariantRepository, IProductRepository productRepository)
        {
            _productVariantRepository = productVariantRepository ?? throw new ArgumentNullException(nameof(productVariantRepository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<ValidationResult> ValidateCreateProductVariant(CreateProductVariantInput input)
        {
            ValidationResult validationResult = new();

            if (string.IsNullOrWhiteSpace(input.Name))
            {
                validationResult.Messages.Add(new(nameof(CreateProductVariantInput.Name), "Debe ingresar un nombre para la variante."));
            }
            else if (input.Name.HasFileInvalidChars())
            {
                validationResult.Messages.Add(new(nameof(CreateProductVariantInput.Name), "El nombre no puede contener caracteres invalidos (<, >, :, \", /, \\, |, ?, *)."));
            }
            else if (!input.Values.Any())
            {
                validationResult.Messages.Add(new(nameof(CreateProductVariantInput.Values), "No se puede crear una variante sin valores."));
            }
            else if (input.Values.GroupBy(x => x).Any(g => g.Count() > 1))
            {
                validationResult.Messages.Add(new(nameof(CreateProductVariantInput.Values), "No pueden haber valores de una variante repetidos."));
            }
            else if (SubCategoriesHasInvalidChars())
            {
                validationResult.Messages.Add(new(nameof(CreateProductVariantInput.Values), "El nombre de los valores no puede contener caracteres invalidos (<, >, :, \", /, \\, |, ?, *)."));
            }
            else
            {
                var product = await _productRepository.GetAsync(input.ProductId);

                if (product is null)
                {
                    validationResult.Messages.Add(new(nameof(CreateProductVariantInput.ProductId), "El producto no existe."));
                }
                else
                {
                    var productVariant = await _productVariantRepository.GetByNameAsync(product, input.Name);

                    if (productVariant is not null)
                    {
                        validationResult.Messages.Add(new(nameof(CreateProductVariantInput.Name), "La variante ya existe."));
                    }
                }
            }

            bool SubCategoriesHasInvalidChars()
            {
                foreach (var item in input.Values)
                {
                    if (item.HasFileInvalidChars())
                        return true;
                }

                return false;
            }

            return validationResult;
        }

        public async Task<ValidationResult> ValidateDeleteProductVariant(DeleteProductVariantInput input)
        {
            ValidationResult validationResult = new();

            var product = await _productRepository.GetAsync(input.ProductId);

            if (product is null)
            {
                validationResult.Messages.Add(new(nameof(DeleteProductVariantInput.ProductId), "El producto no existe."));
            }
            else
            {
                var productVariant = await _productVariantRepository.GetByNameAsync(product, input.Name);

                if (productVariant is null)
                {
                    validationResult.Messages.Add(new(nameof(DeleteProductVariantInput.Name), "La variante no existe."));
                }
            }

            return validationResult;
        }

        public async Task<ValidationResult> ValidateUpdateProductVariant(UpdateProductVariantInput input)
        {
            ValidationResult validationResult = new();

            foreach (var item in input.Variants)
            {
                if (string.IsNullOrWhiteSpace(item.Name))
                {
                    validationResult.Messages.Add(new(nameof(UpdateProductVariantInput.Variants), "Debe ingresar un nombre para la variante."));
                }
                else if (item.Name.HasFileInvalidChars())
                {
                    validationResult.Messages.Add(new(nameof(UpdateProductVariantInput.Variants), "El nombre no puede contener caracteres invalidos (<, >, :, \", /, \\, |, ?, *)."));
                }
                else if (!item.Values.Any())
                {
                    validationResult.Messages.Add(new(nameof(UpdateProductVariantInput.Variants), "No se puede crear una variante sin valores."));
                }
                else if (item.Values.GroupBy(x => x).Any(g => g.Count() > 1))
                {
                    validationResult.Messages.Add(new(nameof(UpdateProductVariantInput.Variants), "No pueden haber valores de una variante repetidos."));
                }
                else if (SubCategoriesHasInvalidChars())
                {
                    validationResult.Messages.Add(new(nameof(UpdateProductVariantInput.Variants), "El nombre de los valores no puede contener caracteres invalidos (<, >, :, \", /, \\, |, ?, *)."));
                }

                bool SubCategoriesHasInvalidChars()
                {
                    foreach (var value in item.Values)
                    {
                        if (value.HasFileInvalidChars())
                            return true;
                    }

                    return false;
                }
            }

            var product = await _productRepository.GetAsync(input.ProductId);

            if (product is null)
            {
                validationResult.Messages.Add(new(nameof(UpdateProductVariantInput.ProductId), "El producto no existe."));
            }

            

            return validationResult;
        }
    }
}
