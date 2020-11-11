using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Dtos.ProductBadges;
using AivenEcommerce.V1.Domain.OperationResults;

namespace AivenEcommerce.V1.Domain.Services
{
    public interface IProductBadgeService
    {
        Task<OperationResult<ProductBadgeDto>> GetByProductAsync(string productId);
    }
}
