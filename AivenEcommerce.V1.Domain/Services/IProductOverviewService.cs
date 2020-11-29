using AivenEcommerce.V1.Domain.Common;
using AivenEcommerce.V1.Domain.Dtos.ProductOverViews;
using AivenEcommerce.V1.Domain.OperationResults;

using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Services
{
    public interface IProductOverviewService : IScopedService
    {
        Task<OperationResult<ProductOverviewDto>> GetByProductAsync(string productId);
    }
}
