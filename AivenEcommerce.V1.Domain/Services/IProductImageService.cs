using System;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Dtos.ProductImages;
using AivenEcommerce.V1.Domain.Entities;

using BusinessLogicEnterprise.Application.OperationResults;

namespace AivenEcommerce.V1.Domain.Services
{
    public interface IProductImageService
    {
        Task<OperationResult<ProductImageDto>> UploadImageAsync(string productId, byte[] image);
        Task<OperationResult> DeleteImageAsync(DeleteProductImageInput input);
        Task<OperationResultEnumerable<ProductImageDto>> GetAllImageAsync(string productId);
    }
}
