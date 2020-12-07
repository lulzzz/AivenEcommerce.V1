using AivenEcommerce.V1.Domain.Shared.Common;
using AivenEcommerce.V1.Domain.Shared.Dtos.Orders;
using AivenEcommerce.V1.Domain.Shared.OperationResults.Validations;

using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Validators
{
    public interface ISaleValidator : IScopedService
    {
        Task<ValidationResult> CreateSaleAsync(CreateSaleInput input);
    }
}
