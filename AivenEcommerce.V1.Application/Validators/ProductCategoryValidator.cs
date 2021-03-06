﻿using AivenEcommerce.V1.Application.Extensions;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Shared.Dtos.ProductCategories;
using AivenEcommerce.V1.Domain.Shared.OperationResults.Validations;
using AivenEcommerce.V1.Domain.Validators;

using System;
using System.Linq;
using System.Threading.Tasks;

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
            ValidationResult validationResult = new();

            if (string.IsNullOrWhiteSpace(input.Name))
            {
                validationResult.Messages.Add(new(nameof(CreateProductCategoryInput.Name), "Debe ingresar un nombre para la categoria."));
            }
            else if (input.Name.HasFileInvalidChars())
            {
                validationResult.Messages.Add(new(nameof(CreateProductCategoryInput.Name), "El nombre no puede contener caracteres invalidos (<, >, :, \", /, \\, |, ?, *)."));
            }
            else if (!input.SubCategories.Any())
            {
                validationResult.Messages.Add(new(nameof(CreateProductCategoryInput.SubCategories), "No se puede crear una categoria sin subcategorias."));
            }
            else if (input.SubCategories.GroupBy(x => x).Any(g => g.Count() > 1))
            {
                validationResult.Messages.Add(new(nameof(CreateProductCategoryInput.Name), "No pueden haber subcategorias repetidas."));
            }
            else if (SubCategoriesHasInvalidChars())
            {
                validationResult.Messages.Add(new(nameof(CreateProductCategoryInput.Name), "El nombre de las subcategorias no puede contener caracteres invalidos (<, >, :, \", /, \\, |, ?, *)."));
            }
            else
            {
                var productCategory = await _repository.GetByNameAsync(input.Name);

                if (productCategory is not null)
                {
                    validationResult.Messages.Add(new(nameof(CreateProductCategoryInput.Name), "La categoria ya existe."));
                }
            }

            bool SubCategoriesHasInvalidChars()
            {
                foreach (var item in input.SubCategories)
                {
                    if (item.HasFileInvalidChars())
                        return true;
                }

                return false;
            }

            return validationResult;
        }

        public async Task<ValidationResult> ValidateDeleteProductCategory(DeleteProductCategoryInput input)
        {
            ValidationResult validationResult = new();

            var productCategory = await _repository.GetByNameAsync(input.Name);

            if (productCategory is null)
            {
                validationResult.Messages.Add(new(nameof(CreateProductCategoryInput.Name), "La categoria no existe."));
            }

            return validationResult;
        }

        public async Task<ValidationResult> ValidateDeleteProductSubCategory(DeleteProductSubCategoryInput input)
        {
            ValidationResult validationResult = new();

            var productCategory = await _repository.GetByNameAsync(input.CategoryName);

            if (productCategory is null)
            {
                validationResult.Messages.Add(new(nameof(UpdateProductSubCategoryNameInput.CategoryName), "La categoria no existe."));
            }
            else if (!productCategory.SubCategories.Any(x => x == input.SubCategoryName))
            {
                validationResult.Messages.Add(new(nameof(CreateProductCategoryInput.Name), "La subcategoria no existe en esta categoria."));
            }

            return validationResult;
        }

        public async Task<ValidationResult> ValidateUpdateProductCategory(UpdateProductCategoryInput input)
        {
            ValidationResult validationResult = new();

            if (input.NewName.HasFileInvalidChars())
            {
                validationResult.Messages.Add(new(nameof(UpdateProductCategoryInput.NewName), "El nombre no puede contener caracteres invalidos (<, >, :, \", /, \\, |, ?, *)."));
            }
            else if (!input.SubCategories.Any())
            {
                validationResult.Messages.Add(new(nameof(UpdateProductCategoryInput.SubCategories), "No se puede crear una categoria sin subcategorias."));
            }
            else if (input.SubCategories.GroupBy(x => x).Any(g => g.Count() > 1))
            {
                validationResult.Messages.Add(new(nameof(UpdateProductCategoryInput.SubCategories), "No pueden haber subcategorias repetidas."));
            }

            var productCategory = await _repository.GetByNameAsync(input.OldName);

            if(productCategory is null)
            {
                validationResult.Messages.Add(new(nameof(UpdateProductCategoryInput.OldName), $"No existe una categoria con el nombre {input.OldName}."));
            }

            productCategory = await _repository.GetByNameAsync(input.NewName);

            if (productCategory is not null)
            {
                validationResult.Messages.Add(new(nameof(UpdateProductCategoryInput.NewName), $"Ya existe una categoria con el nombre {input.NewName}."));
            }

            return validationResult;
        }

        public async Task<ValidationResult> ValidateUpdateProductCategoryNameCategory(UpdateProductCategoryNameInput input)
        {
            ValidationResult validationResult = new();

            if (string.IsNullOrWhiteSpace(input.NewCategoryName))
            {
                validationResult.Messages.Add(new(nameof(CreateProductCategoryInput.Name), "Debe ingresar un nombre para la categoria."));
            }
            else if (input.NewCategoryName.HasFileInvalidChars())
            {
                validationResult.Messages.Add(new(nameof(CreateProductCategoryInput.Name), "El nombre no puede contener caracteres invalidos (<, >, :, \", /, \\, |, ?, *)."));
            }
            else
            {
                var productCategory = await _repository.GetByNameAsync(input.NewCategoryName);

                if (productCategory is not null)
                {
                    validationResult.Messages.Add(new(nameof(CreateProductCategoryInput.Name), "La categoria ya existe."));
                }
            }

            return validationResult;
        }

        public async Task<ValidationResult> ValidateUpdateProductSubCategoryNameCategory(UpdateProductSubCategoryNameInput input)
        {
            ValidationResult validationResult = new();

            if (string.IsNullOrWhiteSpace(input.NewSubCategoryName))
            {
                validationResult.Messages.Add(new(nameof(UpdateProductSubCategoryNameInput.NewSubCategoryName), "Debe ingresar un nombre para la subcategoria."));
            }
            else if (input.NewSubCategoryName.HasFileInvalidChars())
            {
                validationResult.Messages.Add(new(nameof(UpdateProductSubCategoryNameInput.NewSubCategoryName), "El nombre no puede contener caracteres invalidos (<, >, :, \", /, \\, |, ?, *)."));
            }
            else
            {
                var productCategory = await _repository.GetByNameAsync(input.CategoryName);

                if (productCategory is null)
                {
                    validationResult.Messages.Add(new(nameof(UpdateProductSubCategoryNameInput.CategoryName), "La categoria no existe."));
                }
                else if (productCategory.SubCategories.Any(x => x == input.NewSubCategoryName))
                {
                    validationResult.Messages.Add(new(nameof(CreateProductCategoryInput.Name), "La subcategoria ya existe en esta categoria."));
                }
            }

            return validationResult;
        }
    }
}
