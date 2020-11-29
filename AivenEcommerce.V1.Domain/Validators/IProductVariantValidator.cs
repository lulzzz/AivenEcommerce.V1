using AivenEcommerce.V1.Application.Validations;
using AivenEcommerce.V1.Domain.Common;
using AivenEcommerce.V1.Domain.Dtos.ProductVariants;

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
