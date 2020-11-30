using AivenEcommerce.V1.Domain.Entities;
using AivenEcommerce.V1.Domain.Repositories;
using AivenEcommerce.V1.Domain.Shared.Dtos.Products;
using AivenEcommerce.V1.Domain.Shared.OperationResults.Validations;
using AivenEcommerce.V1.Domain.Validators;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Application.Validators
{
    public class ProductValidator : IProductValidator
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductValidator(IProductRepository productRepository, IProductCategoryRepository productCategoryRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _productCategoryRepository = productCategoryRepository ?? throw new ArgumentNullException(nameof(productCategoryRepository));
        }

        public async Task<ValidationResult> ValidateCreateProduct(CreateProductInput input)
        {
            ValidationResult validationResult = new();

            if (string.IsNullOrWhiteSpace(input.Name))
            {
                validationResult.Messages.Add(new(nameof(CreateProductInput.Name), "El nombre no puede estar vacio."));
            }
            else
            {
                Product product = _productRepository.GetByName(input.Name);
                if (product is not null)
                {
                    validationResult.Messages.Add(new(nameof(CreateProductInput.Name), "Ya existe un producto con este nombre."));
                }
            }

            if (string.IsNullOrWhiteSpace(input.Description))
            {
                validationResult.Messages.Add(new(nameof(CreateProductInput.Description), "La description no puede estar vacia."));
            }

            if (input.Price <= 0)
            {
                validationResult.Messages.Add(new(nameof(CreateProductInput.Price), "El precio debe ser mayor a cero."));
            }

            if (input.Price < input.Cost)
            {
                validationResult.Messages.Add(new(nameof(CreateProductInput.Price), "El precio no puede ser menor que el costo."));
            }



            if (string.IsNullOrWhiteSpace(input.Category))
            {
                validationResult.Messages.Add(new(nameof(CreateProductInput.Category), "Debe seleccionar una categoria."));
            }
            else
            {
                var category = await _productCategoryRepository.GetByNameAsync(input.Category);
                if (category is null)
                {
                    validationResult.Messages.Add(new(nameof(CreateProductInput.Category), "La categoria no existe."));
                }
                else if (string.IsNullOrWhiteSpace(input.SubCategory))
                {
                    validationResult.Messages.Add(new(nameof(CreateProductInput.SubCategory), "Debe seleccionar una subcategoria."));
                }
                else if (!category.SubCategories.Any(x => x == input.SubCategory))
                {
                    validationResult.Messages.Add(new(nameof(CreateProductInput.SubCategory), "La subcategoria no existe."));
                }

            }

            return validationResult;
        }

        public async Task<ValidationResult> ValidateDeleteProduct(DeleteProductInput input)
        {
            ValidationResult validationResult = new();

            Product product = await _productRepository.GetAsync(input.Id);

            if (product is null)
            {
                validationResult.Messages.Add(new(nameof(DeleteProductInput.Id), "El producto no existe."));
            }

            return validationResult;
        }

        public async Task<ValidationResult> ValidateGetProduct(GetProductInput input)
        {
            ValidationResult validationResult = new();

            Product product = await _productRepository.GetAsync(input.Id);

            if (product is null)
            {
                validationResult.Messages.Add(new(nameof(GetProductInput.Id), "El producto no existe."));
            }

            return validationResult;
        }

        public async Task<ValidationResult> ValidateUpdateProduct(UpdateProductInput input)
        {
            ValidationResult validationResult = new();

            Product product = await _productRepository.GetAsync(input.Id);

            if (product is null)
            {
                validationResult.Messages.Add(new(nameof(UpdateProductInput.Id), "El producto no existe."));
            }
            else
            {
                if (string.IsNullOrWhiteSpace(input.Name))
                {
                    validationResult.Messages.Add(new(nameof(UpdateProductInput.Name), "El nombre no puede estar vacio."));
                }
                else
                {
                    product = _productRepository.GetByName(input.Name);

                    if (product != null && product.Id != input.Id)
                    {
                        validationResult.Messages.Add(new(nameof(UpdateProductInput.Name), "Ya existe un producto con este nombre."));
                    }
                }
                if (input.Price <= 0)
                {
                    validationResult.Messages.Add(new(nameof(UpdateProductInput.Price), "El precio debe ser mayor a cero."));
                }

                if (input.Price < input.Cost)
                {
                    validationResult.Messages.Add(new(nameof(UpdateProductInput.Price), "El precio no puede ser menor que el costo."));
                }

                if (string.IsNullOrWhiteSpace(input.Category))
                {
                    validationResult.Messages.Add(new(nameof(UpdateProductInput.Category), "Debe seleccionar una categoria."));
                }

                if (string.IsNullOrWhiteSpace(input.SubCategory))
                {
                    validationResult.Messages.Add(new(nameof(UpdateProductInput.SubCategory), "Debe seleccionar una subcategoria."));
                }

                if (input.Thumbnail is null)
                {
                    validationResult.Messages.Add(new(nameof(UpdateProductInput.Thumbnail), "Debe subir una imagen para el producto."));
                }
            }

            return validationResult;
        }

        public async Task<ValidationResult> ValidateUpdateProductAvailability(UpdateProductAvailabilityInput input)
        {
            ValidationResult validationResult = new();

            Product product = await _productRepository.GetAsync(input.ProductId);

            if (product is null)
            {
                validationResult.Messages.Add(new(nameof(UpdateProductAvailabilityInput.ProductId), "El producto no existe."));
            }
            else if (input.IsActive)
            {
                if (input.Stock <= 0)
                {
                    validationResult.Messages.Add(new(nameof(UpdateProductAvailabilityInput.Stock), "No se puede activar un producto que no tiene stock."));
                }
                if (string.IsNullOrWhiteSpace(product.Name))
                {
                    validationResult.Messages.Add(new(nameof(UpdateProductAvailabilityInput.Stock), "No se puede activar un producto que no tiene nombre."));
                }
                if (string.IsNullOrWhiteSpace(product.Category))
                {
                    validationResult.Messages.Add(new(nameof(UpdateProductAvailabilityInput.Stock), "No se puede activar un producto que no tiene categoria."));
                }
                if (string.IsNullOrWhiteSpace(product.SubCategory))
                {
                    validationResult.Messages.Add(new(nameof(UpdateProductAvailabilityInput.Stock), "No se puede activar un producto que no tiene subcategoria."));
                }
                if (product.Cost < 0)
                {
                    validationResult.Messages.Add(new(nameof(UpdateProductAvailabilityInput.Stock), "No se puede activar un producto cuyo costo es menor a cero."));
                }
                if (product.Price <= 0)
                {
                    validationResult.Messages.Add(new(nameof(UpdateProductInput.Price), "No se puede activar un producto cuyo precio es mayor a cero."));
                }
                if (product.Price < product.Cost)
                {
                    validationResult.Messages.Add(new(nameof(UpdateProductInput.Price), "No se puede activar un producto cuyo precio es menor que el costo."));
                }
                if (product.Thumbnail is null)
                {
                    validationResult.Messages.Add(new(nameof(UpdateProductInput.Thumbnail), "No se puede activar un producto sin una imagen principal."));
                }
            }

            return validationResult;
        }

        public async Task<ValidationResult> ValidateUpdateProductBadge(UpdateProductBadgeInput input)
        {
            ValidationResult validationResult = new();

            Product product = await _productRepository.GetAsync(input.ProductId);

            if (product is null)
            {
                validationResult.Messages.Add(new(nameof(UpdateProductBadgeInput.ProductId), "El producto no existe."));
            }

            return validationResult;
        }

        public async Task<ValidationResult> ValidateUpdateProductCategory(UpdateProductCategorySubCategoryInput input)
        {
            ValidationResult validationResult = new();

            Product product = await _productRepository.GetAsync(input.ProductId);

            if (product is null)
            {
                validationResult.Messages.Add(new(nameof(UpdateProductCategorySubCategoryInput.ProductId), "El producto no existe."));
            }
            else
            {
                ProductCategory category = await _productCategoryRepository.GetByNameAsync(input.Category);

                if (category is null)
                {
                    validationResult.Messages.Add(new(nameof(UpdateProductInput.Id), "La categoria no existe."));
                }
                else if (!category.SubCategories.Any(x => x.Trim() == input.SubCategory.Trim()))
                {
                    validationResult.Messages.Add(new(nameof(UpdateProductInput.Id), "La subcategoria no existe."));
                }
            }

            return validationResult;
        }

        public async Task<ValidationResult> ValidateUpdateProductCostPrice(UpdateProductCostPriceInput input)
        {
            ValidationResult validationResult = new();

            Product product = await _productRepository.GetAsync(input.ProductId);

            if (product is null)
            {
                validationResult.Messages.Add(new(nameof(UpdateProductCostPriceInput.ProductId), "El producto no existe."));
            }
            else
            {
                if (input.Cost < 0)
                {
                    validationResult.Messages.Add(new(nameof(UpdateProductCostPriceInput.Cost), "El costo debe ser mayor o igual a cero."));
                }

                if (input.Price <= 0)
                {
                    validationResult.Messages.Add(new(nameof(UpdateProductCostPriceInput.Price), "El precio debe ser mayor a cero."));
                }

                if (input.Price < input.Cost)
                {
                    validationResult.Messages.Add(new(nameof(UpdateProductCostPriceInput.Price), "El precio no puede ser menor que el costo."));
                }
            }

            return validationResult;
        }

        public async Task<ValidationResult> ValidateUpdateProductNameDescription(UpdateProductNameDescriptionInput input)
        {
            ValidationResult validationResult = new();

            Product product = await _productRepository.GetAsync(input.ProductId);

            if (product is null)
            {
                validationResult.Messages.Add(new(nameof(UpdateProductNameDescriptionInput.ProductId), "El producto no existe."));
            }
            else if (string.IsNullOrWhiteSpace(input.Name))
            {
                validationResult.Messages.Add(new(nameof(UpdateProductNameDescriptionInput.Name), "El nombre del producto no puede estar vacio."));
            }
            else if (string.IsNullOrWhiteSpace(input.Description))
            {
                validationResult.Messages.Add(new(nameof(UpdateProductNameDescriptionInput.Description), "La descripción del producto no puede estar vacio."));
            }

            return validationResult;
        }
    }
}
