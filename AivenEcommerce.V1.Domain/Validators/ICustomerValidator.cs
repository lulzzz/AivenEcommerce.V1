﻿using AivenEcommerce.V1.Domain.Shared.Common;
using AivenEcommerce.V1.Domain.Shared.Dtos.Customers;
using AivenEcommerce.V1.Domain.Shared.OperationResults.Validations;

using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Validators
{
    public interface ICustomerValidator : IScopedService
    {
        Task<ValidationResult> ValidateCreateCustomer(CreateCustomerInput input);
        Task<ValidationResult> ValidateUpdateCustomer(UpdateCustomerInput input);
        Task<ValidationResult> ValidateDeleteCustomer(DeleteCustomerInput input);
    }
}
