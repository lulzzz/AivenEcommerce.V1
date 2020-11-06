using System;
using System.Threading.Tasks;

using AivenEcommerce.V1.Application.Validations;

using AivenEcommerce.V1.Domain.Dtos.Products;
using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Enums;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Validators;

namespace AivenEcommerce.V1.Application.Validators
{
    public class ProductValidator : IProductValidator
    {
        private readonly IProductRepository _productRepository;

        public ProductValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public ValidationResult ValidateCreateProduct(CreateProductInput input)
        {
            ValidationResult validationResult = new ValidationResult();

            if (string.IsNullOrWhiteSpace(input.Name))
            {
                validationResult.Messages.Add(new ValidationMessage(nameof(CreateProductInput.Name), "El nombre no puede estar vacio."));
            }
            else
            {
                Product? product = _productRepository.GetByName(input.Name);
                if (product != null)
                {
                    validationResult.Messages.Add(new ValidationMessage(nameof(CreateProductInput.Name), "Ya existe un producto con este nombre."));
                }
            }

            if (string.IsNullOrWhiteSpace(input.Description))
            {
                validationResult.Messages.Add(new ValidationMessage(nameof(CreateProductInput.Description), "La description no puede estar vacia."));
            }

            if (input.Price <= 0)
            {
                validationResult.Messages.Add(new ValidationMessage(nameof(CreateProductInput.Price), "El precio debe ser mayor a cero."));
            }

            if (input.Price < input.Cost)
            {
                validationResult.Messages.Add(new ValidationMessage(nameof(CreateProductInput.Price), "El precio no puede ser menor que el costo."));
            }

            if (input.Category ==  ProductCategory.None)
            {
                validationResult.Messages.Add(new ValidationMessage(nameof(CreateProductInput.Category), "Debe seleccionar una categoria."));
            }

            if (input.SubCategory == ProductSubCategory.None)
            {
                validationResult.Messages.Add(new ValidationMessage(nameof(CreateProductInput.SubCategory), "Debe seleccionar una subcategoria."));
            }

            if (input.Thumbnail is null)
            {
                validationResult.Messages.Add(new ValidationMessage(nameof(CreateProductInput.Thumbnail), "Debe subir una imagen para el producto."));
            }

            return validationResult;
        }

        public async Task<ValidationResult> ValidateDeleteProduct(DeleteProductInput input)
        {
            ValidationResult validationResult = new ValidationResult();

            Product? product = await _productRepository.GetAsync(input.Id);

            if (product is null)
            {
                validationResult.Messages.Add(new ValidationMessage(nameof(DeleteProductInput.Id), "El producto no existe."));
            }

            return validationResult;
        }

        public async Task<ValidationResult> ValidateGetProduct(GetProductInput input)
        {
            ValidationResult validationResult = new ValidationResult();

            Product? product = await _productRepository.GetAsync(input.Id);

            if (product is null)
            {
                validationResult.Messages.Add(new ValidationMessage(nameof(GetProductInput.Id), "El producto no existe."));
            }

            return validationResult;
        }

        public async Task<ValidationResult> ValidateUpdateProduct(UpdateProductInput input)
        {
            ValidationResult validationResult = new ValidationResult();

            Product? product = await _productRepository.GetAsync(input.Id);

            if(product is null)
            {
                validationResult.Messages.Add(new ValidationMessage(nameof(UpdateProductInput.Id), "El producto no existe."));
            }
            else
            {
                if (string.IsNullOrWhiteSpace(input.Name))
                {
                    validationResult.Messages.Add(new ValidationMessage(nameof(UpdateProductInput.Name), "El nombre no puede estar vacio."));
                }
                else
                {
                    product = _productRepository.GetByName(input.Name);

                    if (product != null && product.Id != input.Id)
                    {
                        validationResult.Messages.Add(new ValidationMessage(nameof(UpdateProductInput.Name), "Ya existe un producto con este nombre."));
                    }
                }

                if (string.IsNullOrWhiteSpace(input.Description))
                {
                    validationResult.Messages.Add(new ValidationMessage(nameof(UpdateProductInput.Description), "La description no puede estar vacia."));
                }

                if (input.Price <= 0)
                {
                    validationResult.Messages.Add(new ValidationMessage(nameof(UpdateProductInput.Price), "El precio debe ser mayor a cero."));
                }

                if (input.Price < input.Cost)
                {
                    validationResult.Messages.Add(new ValidationMessage(nameof(UpdateProductInput.Price), "El precio no puede ser menor que el costo."));
                }

                if (input.Category == ProductCategory.None)
                {
                    validationResult.Messages.Add(new ValidationMessage(nameof(UpdateProductInput.Category), "Debe seleccionar una categoria."));
                }

                if (input.SubCategory == ProductSubCategory.None)
                {
                    validationResult.Messages.Add(new ValidationMessage(nameof(UpdateProductInput.SubCategory), "Debe seleccionar una subcategoria."));
                }

                if (input.Thumbnail is null)
                {
                    validationResult.Messages.Add(new ValidationMessage(nameof(UpdateProductInput.Thumbnail), "Debe subir una imagen para el producto."));
                }
            }

            return validationResult;
        }
    }
}
