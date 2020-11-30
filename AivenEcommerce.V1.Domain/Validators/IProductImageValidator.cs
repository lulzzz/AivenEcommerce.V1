using AivenEcommerce.V1.Domain.Shared.Common;
using AivenEcommerce.V1.Domain.Shared.Dtos.ProductImages;
using AivenEcommerce.V1.Domain.Shared.OperationResults.Validations;

using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Validators
{
    public interface IProductImageValidator : IScopedService
    {
        Task<ValidationResult> ValidateDeleteProductImage(DeleteProductImageInput input);
    }
}
