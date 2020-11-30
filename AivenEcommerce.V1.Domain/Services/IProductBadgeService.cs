using AivenEcommerce.V1.Domain.Shared.Common;
using AivenEcommerce.V1.Domain.Shared.Dtos.ProductBadges;
using AivenEcommerce.V1.Domain.Shared.OperationResults;

using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Services
{
    public interface IProductBadgeService : IScopedService
    {
        Task<OperationResult<ProductBadgeDto>> GetByProductAsync(string productId);
    }
}
