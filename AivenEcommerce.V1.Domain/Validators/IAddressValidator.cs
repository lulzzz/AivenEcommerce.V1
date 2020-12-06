using AivenEcommerce.V1.Domain.Shared.Common;
using AivenEcommerce.V1.Domain.Shared.Dtos.Addresses;
using AivenEcommerce.V1.Domain.Shared.OperationResults.Validations;

using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Validators
{
    public interface IAddressValidator : IScopedService
    {
        Task<ValidationResult> ValidateCreateAddressAsync(CreateAddressInput input);
        Task<ValidationResult> ValidateUpdateAddressAsync(UpdateAddressInput input);
        Task<ValidationResult> ValidateDeleteAddressAsync(DeleteAddressInput input);
    }
}

