using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Dtos.Customers;
using AivenEcommerce.V1.Domain.OperationResults;

namespace AivenEcommerce.V1.Domain.Services
{
    public interface ICustomerService
    {
        Task<OperationResultEnumerable<CustomerDto>> GetAllAsync();
        Task<OperationResult<CustomerDto>> GetCustomerAsync(string email);
        Task<OperationResult<CustomerDto>> CreateCustomerAsync(CreateCustomerInput input);
        Task<OperationResult<CustomerDto>> UpdateCustomerAsync(UpdateCustomerInput input);
        Task<OperationResult> DeleteCustomerAsync(DeleteCustomerInput input);
    }
}
