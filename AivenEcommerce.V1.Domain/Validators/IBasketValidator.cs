using System.Threading.Tasks;

using AivenEcommerce.V1.Application.Validations;
using AivenEcommerce.V1.Domain.Common;
using AivenEcommerce.V1.Domain.Dtos.Baskets;

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
