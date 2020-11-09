using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Dtos.Products;

using BusinessLogicEnterprise.Application.OperationResults;

namespace AivenEcommerce.V1.Domain.Services
{
    public interface IProductService
    {
        Task<OperationResult<ProductDto>> GetAsync(string id);
        Task<OperationResultEnumerable<ProductDto>> GetAllAsync();
        Task<OperationResult<ProductDto>> CreateAsync(CreateProductInput input);
        Task<OperationResult<ProductDto>> UpdateAsync(UpdateProductInput input);
        Task<OperationResult> DeleteAsync(DeleteProductInput input);
        Task<OperationResult<ProductDto>> UpdateMainImageAsync(UpdateProductMainImageInput input);
        Task<OperationResult<ProductDto>> UpdateProductCategoryAsync(UpdateProductCategorySubCategoryInput input);
        Task<OperationResult<ProductDto>> UpdateProductCostPriceAsync(UpdateProductCostPriceInput input);

    }
}
