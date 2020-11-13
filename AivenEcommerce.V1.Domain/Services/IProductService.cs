using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Dtos.Products;
using AivenEcommerce.V1.Domain.OperationResults;

namespace AivenEcommerce.V1.Domain.Services
{
    public interface IProductService
    {
        Task<OperationResult<ProductDto>> GetAsync(string id);
        OperationResultEnumerable<ProductDto> GetByCategory(string category);
        OperationResultEnumerable<ProductDto> GetByCategory(string category, string subcategory);
        Task<OperationResultEnumerable<ProductDto>> GetAllAsync();
        Task<OperationResult<ProductDto>> CreateAsync(CreateProductInput input);
        Task<OperationResult<ProductDto>> UpdateAsync(UpdateProductInput input);
        Task<OperationResult> DeleteAsync(DeleteProductInput input);
        Task<OperationResult<ProductDto>> UpdateMainImageAsync(UpdateProductMainImageInput input);
        Task<OperationResult<ProductDto>> UpdateProductCategoryAsync(UpdateProductCategorySubCategoryInput input);
        Task<OperationResult<ProductDto>> UpdateProductCostPriceAsync(UpdateProductCostPriceInput input);
        Task<OperationResult<ProductDto>> UpdateProductAvailabilityAsync(UpdateProductAvailabilityInput input);
        Task<OperationResult<ProductDto>> UpdateProductNameDescriptionAsync(UpdateProductNameDescriptionInput input);
        Task<OperationResult<ProductDto>> UpdateProductBadgeAsync(UpdateProductBadgeInput input);

    }
}
