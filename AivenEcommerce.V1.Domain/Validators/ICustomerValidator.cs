using AivenEcommerce.V1.Application.Validations;
using AivenEcommerce.V1.Domain.Common;
using AivenEcommerce.V1.Domain.Dtos.Customers;

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
