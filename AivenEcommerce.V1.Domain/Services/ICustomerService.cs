using AivenEcommerce.V1.Domain.Shared.Common;
using AivenEcommerce.V1.Domain.Shared.Dtos.Customers;
using AivenEcommerce.V1.Domain.Shared.OperationResults;

using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Services
{
    public interface ICustomerService : IScopedService
    {
        Task<OperationResultEnumerable<CustomerDto>> GetAllAsync();
        Task<OperationResult<CustomerDto>> GetCustomerAsync(string email);
        Task<OperationResult<CustomerDto>> CreateCustomerAsync(CreateCustomerInput input);
        Task<OperationResult<CustomerDto>> UpdateCustomerAsync(UpdateCustomerInput input);
        Task<OperationResult> DeleteCustomerAsync(DeleteCustomerInput input);
    }
}
