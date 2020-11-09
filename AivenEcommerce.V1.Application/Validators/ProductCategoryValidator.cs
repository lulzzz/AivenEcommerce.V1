using System;
using System.Linq;
using System.Threading.Tasks;

using AivenEcommerce.V1.Application.Extensions;
using AivenEcommerce.V1.Application.Validations;
using AivenEcommerce.V1.Domain.Dtos.ProductCategories;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Validators;

namespace AivenEcommerce.V1.Application.Validators
{
    public class ProductCategoryValidator : IProductCategoryValidator
    {
        private readonly IProductCategoryRepository _repository;

        public ProductCategoryValidator(IProductCategoryRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<ValidationResult> ValidateCreateProductCategory(CreateProductCategoryInput input)
        {
            ValidationResult validationResult = new ValidationResult();

            if (input.Name.HasFileInvalidChars())
            {
                validationResult.Messages.Add(new ValidationMessage(nameof(CreateProductCategoryInput.Name), "El nombre no puede contener caracteres invalidos (<, >, :, \", /, \\, |, ?, *)."));
            }
            else if (input.SubCategories.GroupBy(x => x).Any(g => g.Count() > 1))
            {
                validationResult.Messages.Add(new ValidationMessage(nameof(CreateProductCategoryInput.Name), "No pueden ver subcategorias repetidas."));
            }
            else
            {
                var productCategory = await _repository.GetByNameAsync(input.Name);

                if (productCategory != null)
                {
                    validationResult.Messages.Add(new ValidationMessage(nameof(CreateProductCategoryInput.Name), "La categoria ya existe."));
                }
            }

            return validationResult;
        }

        public async Task<ValidationResult> ValidateDeleteProductCategory(DeleteProductCategoryInput input)
        {
            ValidationResult validationResult = new ValidationResult();

            var productCategory = await _repository.GetByNameAsync(input.Name);

            if (productCategory is null)
            {
                validationResult.Messages.Add(new ValidationMessage(nameof(CreateProductCategoryInput.Name), "La categoria no existe."));
            }

            return validationResult;
        }

        public async Task<ValidationResult> ValidateUpdateProductCategory(UpdateProductCategoryInput input)
        {
            ValidationResult validationResult = new ValidationResult();

            if (input.Name.HasFileInvalidChars())
            {
                validationResult.Messages.Add(new ValidationMessage(nameof(CreateProductCategoryInput.Name), "El nombre no puede contener caracteres invalidos (<, >, :, \", /, \\, |, ?, *)."));
            }
            else if (input.SubCategories.GroupBy(x => x).Any(g => g.Count() > 1))
            {
                validationResult.Messages.Add(new ValidationMessage(nameof(CreateProductCategoryInput.Name), "No pueden ver subcategorias repetidas."));
            }

            return validationResult;
        }
    }
}
