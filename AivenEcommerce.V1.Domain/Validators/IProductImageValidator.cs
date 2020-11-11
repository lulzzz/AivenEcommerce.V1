using System.Threading.Tasks;

using AivenEcommerce.V1.Application.Validations;
using AivenEcommerce.V1.Domain.Dtos.ProductImages;

namespace AivenEcommerce.V1.Domain.Validators
{
    public interface IProductImageValidator
    {
        Task<ValidationResult> ValidateDeleteProductImage(DeleteProductImageInput input);
    }
}
