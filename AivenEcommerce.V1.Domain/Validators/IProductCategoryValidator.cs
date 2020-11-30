using AivenEcommerce.V1.Domain.Shared.Common;
using AivenEcommerce.V1.Domain.Shared.Dtos.ProductCategories;
using AivenEcommerce.V1.Domain.Shared.OperationResults.Validations;

using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Validators
{
    public interface IProductCategoryValidator : IScopedService
    {
        Task<ValidationResult> ValidateCreateProductCategory(CreateProductCategoryInput input);
        Task<ValidationResult> ValidateUpdateProductCategory(UpdateProductCategoryInput input);
        Task<ValidationResult> ValidateDeleteProductCategory(DeleteProductCategoryInput input);
        Task<ValidationResult> ValidateDeleteProductSubCategory(DeleteProductSubCategoryInput input);
        Task<ValidationResult> ValidateUpdateProductCategoryNameCategory(UpdateProductCategoryNameInput input);
        Task<ValidationResult> ValidateUpdateProductSubCategoryNameCategory(UpdateProductSubCategoryNameInput input);
    }
}
