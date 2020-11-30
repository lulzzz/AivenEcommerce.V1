using AivenEcommerce.V1.Domain.Shared.Dtos.Addresses;
using AivenEcommerce.V1.Domain.Shared.OperationResults.Validations;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Validators
{
    public interface IAddressValidator
    {
        Task<ValidationResult> ValidateCreateAddressAsync(CreateAddressInput input);
        Task<ValidationResult> ValidateUpdateAddressAsync(UpdateAddressInput input);
        Task<ValidationResult> ValidateDeleteAddressAsync(DeleteAddressInput input);
    }
}

