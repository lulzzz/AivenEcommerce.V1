using AivenEcommerce.V1.Domain.Shared.Common;
using AivenEcommerce.V1.Domain.Shared.Dtos.ProductVariants;
using AivenEcommerce.V1.Domain.Shared.OperationResults.Validations;

using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Validators
{
    public interface IProductVariantValidator : IScopedService
    {
        Task<ValidationResult> ValidateCreateProductVariant(CreateProductVariantInput input);
        Task<ValidationResult> ValidateUpdateProductVariant(UpdateProductVariantInput input);
        Task<ValidationResult> ValidateDeleteProductVariant(DeleteProductVariantInput input);
    }
}
