using System.Threading.Tasks;

using AivenEcommerce.V1.Application.Validations;
using AivenEcommerce.V1.Domain.Dtos.ProductCategories;

namespace AivenEcommerce.V1.Domain.Validators
{
    public interface IProductCategoryValidator
    {
        Task<ValidationResult> ValidateCreateProductCategory(CreateProductCategoryInput input);
        Task<ValidationResult> ValidateUpdateProductCategory(UpdateProductCategoryInput input);
        Task<ValidationResult> ValidateDeleteProductCategory(DeleteProductCategoryInput input);
        Task<ValidationResult> ValidateDeleteProductSubCategory(DeleteProductSubCategoryInput input);
        Task<ValidationResult> ValidateUpdateProductCategoryNameCategory(UpdateProductCategoryNameInput input);
        Task<ValidationResult> ValidateUpdateProductSubCategoryNameCategory(UpdateProductSubCategoryNameInput input);
    }
}
