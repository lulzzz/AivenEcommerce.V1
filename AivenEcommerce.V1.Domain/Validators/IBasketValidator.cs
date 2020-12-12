using AivenEcommerce.V1.Domain.Shared.Common;
using AivenEcommerce.V1.Domain.Shared.Dtos.Baskets;
using AivenEcommerce.V1.Domain.Shared.OperationResults.Validations;

using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Validators
{
    public interface IBasketValidator : IScopedService
    {
        Task<ValidationResult> ValidateAddBasketProductAsync(AddBasketProductInput input);
        Task<ValidationResult> ValidateRemoveBasketProductAsync(RemoveBasketProductInput input);
        Task<ValidationResult> ValidateRemoveAllBasketAsync(RemoveAllBasketInput input);
        Task<ValidationResult> ValidateUpdateBasketAsync(UpdateBasketInput input);
    }
}
