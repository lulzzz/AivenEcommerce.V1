using AivenEcommerce.V1.Domain.Shared.Common;
using AivenEcommerce.V1.Domain.Shared.Dtos.ProductVariants;
using AivenEcommerce.V1.Domain.Shared.OperationResults;

using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Services
{
    public interface IProductVariantService : IScopedService
    {
        Task<OperationResult<ProductVariantDto>> GetAsync(string productId, string name);
        Task<OperationResultEnumerable<ProductVariantDto>> GetAllAsync(string productId);
        Task<OperationResult<ProductVariantDto>> CreateAsync(CreateProductVariantInput input);
        Task<OperationResultEnumerable<ProductVariantDto>> UpdateAsync(UpdateProductVariantInput input);
        Task<OperationResult> DeleteAsync(DeleteProductVariantInput input);
    }
}
