using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Common;
using AivenEcommerce.V1.Domain.Dtos.ProductVariants;
using AivenEcommerce.V1.Domain.OperationResults;

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
