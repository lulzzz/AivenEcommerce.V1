using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Dtos.ProductVariants;
using AivenEcommerce.V1.Domain.OperationResults;

namespace AivenEcommerce.V1.Domain.Services
{
    public interface IProductVariantService
    {
        Task<OperationResult<ProductVariantDto>> GetAsync(string productId, string name);
        Task<OperationResultEnumerable<ProductVariantDto>> GetAllAsync(string productId);
        Task<OperationResult<ProductVariantDto>> CreateAsync(CreateProductVariantInput input);
        Task<OperationResult<ProductVariantDto>> UpdateAsync(UpdateProductVariantInput input);
        Task<OperationResult> DeleteAsync(DeleteProductVariantInput input);
    }
}
