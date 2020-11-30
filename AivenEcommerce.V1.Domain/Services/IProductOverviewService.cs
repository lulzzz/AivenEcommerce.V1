using AivenEcommerce.V1.Domain.Shared.Common;
using AivenEcommerce.V1.Domain.Shared.Dtos.ProductOverViews;
using AivenEcommerce.V1.Domain.Shared.OperationResults;

using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Services
{
    public interface IProductOverviewService : IScopedService
    {
        Task<OperationResult<ProductOverviewDto>> GetByProductAsync(string productId);
    }
}
