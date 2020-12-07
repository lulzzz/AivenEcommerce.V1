using AivenEcommerce.V1.Domain.Shared.Common;
using AivenEcommerce.V1.Domain.Shared.Dtos.Orders;
using AivenEcommerce.V1.Domain.Shared.Dtos.Sales;
using AivenEcommerce.V1.Domain.Shared.OperationResults;

using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Services
{
    public interface ISaleService : IScopedService
    {
        Task<OperationResult<SaleOrderDto>> CreateSaleAsync(CreateSaleInput input);
    }
}
