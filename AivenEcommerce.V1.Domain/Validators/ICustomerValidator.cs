using System.Threading.Tasks;

using AivenEcommerce.V1.Application.Validations;
using AivenEcommerce.V1.Domain.Dtos.Customers;

namespace AivenEcommerce.V1.Domain.Validators
{
    public interface ICustomerValidator
    {
        Task<ValidationResult> ValidateCreateCustomer(CreateCustomerInput input);
        Task<ValidationResult> ValidateUpdateCustomer(UpdateCustomerInput input);
        Task<ValidationResult> ValidateDeleteCustomer(DeleteCustomerInput input);
    }
}
