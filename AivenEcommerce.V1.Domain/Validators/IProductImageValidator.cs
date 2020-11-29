using AivenEcommerce.V1.Application.Validations;
using AivenEcommerce.V1.Domain.Common;
using AivenEcommerce.V1.Domain.Dtos.ProductImages;

using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Validators
{
    public interface IProductImageValidator : IScopedService
    {
        Task<ValidationResult> ValidateDeleteProductImage(DeleteProductImageInput input);
    }
}
