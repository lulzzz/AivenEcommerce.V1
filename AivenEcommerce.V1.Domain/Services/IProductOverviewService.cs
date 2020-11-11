using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Dtos.ProductOverViews;
using AivenEcommerce.V1.Domain.OperationResults;

namespace AivenEcommerce.V1.Domain.Services
{
    public interface IProductOverviewService
    {
        Task<OperationResult<ProductOverviewDto>> GetByProductAsync(string productId);
    }
}
