using AivenEcommerce.V1.Domain.Shared.Common;
using AivenEcommerce.V1.Domain.Shared.Dtos.Baskets;
using AivenEcommerce.V1.Domain.Shared.OperationResults.Validations;

using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Validators
{
    public interface IBasketValidator : IScopedService
    {
        Task<ValidationResult> ValidateAddBasketProduct(AddBasketProductInput input);
        Task<ValidationResult> ValidateRemoveBasketProduct(RemoveBasketProductInput input);
        Task<ValidationResult> ValidateRemoveAllBasket(RemoveAllBasketInput input);
        Task<ValidationResult> ValidateUpdateBasket(UpdateBasketInput input);
    }
}
