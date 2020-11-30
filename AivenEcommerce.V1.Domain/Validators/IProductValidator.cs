using AivenEcommerce.V1.Domain.Shared.Common;
using AivenEcommerce.V1.Domain.Shared.Dtos.Products;
using AivenEcommerce.V1.Domain.Shared.OperationResults.Validations;

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
