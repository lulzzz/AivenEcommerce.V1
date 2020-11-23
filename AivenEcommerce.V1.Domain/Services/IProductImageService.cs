using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Common;
using AivenEcommerce.V1.Domain.Dtos.ProductImages;
using AivenEcommerce.V1.Domain.OperationResults;

namespace AivenEcommerce.V1.Domain.Services
{
    public interface IProductImageService : IScopedService
    {
        Task<OperationResult<ProductImageDto>> UploadImageAsync(string productId, byte[] image);
        Task<OperationResult> DeleteImageAsync(DeleteProductImageInput input);
        Task<OperationResultEnumerable<ProductImageDto>> GetAllImageAsync(string productId);
    }
}
