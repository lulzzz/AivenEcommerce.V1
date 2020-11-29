﻿using AivenEcommerce.V1.Application.Validations;
using AivenEcommerce.V1.Domain.Common;
using AivenEcommerce.V1.Domain.Dtos.Products;

using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Validators
{
    public interface IProductValidator : IScopedService
    {
        Task<ValidationResult> ValidateCreateProduct(CreateProductInput input);
        Task<ValidationResult> ValidateUpdateProduct(UpdateProductInput input);
        Task<ValidationResult> ValidateDeleteProduct(DeleteProductInput input);
        Task<ValidationResult> ValidateGetProduct(GetProductInput input);
        Task<ValidationResult> ValidateUpdateProductCostPrice(UpdateProductCostPriceInput input);
        Task<ValidationResult> ValidateUpdateProductCategory(UpdateProductCategorySubCategoryInput input);
        Task<ValidationResult> ValidateUpdateProductAvailability(UpdateProductAvailabilityInput input);
        Task<ValidationResult> ValidateUpdateProductNameDescription(UpdateProductNameDescriptionInput input);
        Task<ValidationResult> ValidateUpdateProductBadge(UpdateProductBadgeInput input);


    }
}
