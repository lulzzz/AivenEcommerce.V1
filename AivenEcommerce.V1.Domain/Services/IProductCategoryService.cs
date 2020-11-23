using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Common;
using AivenEcommerce.V1.Domain.Dtos.ProductCategories;
using AivenEcommerce.V1.Domain.OperationResults;

namespace AivenEcommerce.V1.Domain.Services
{
    public interface IProductCategoryService : IScopedService
    {
        Task<OperationResult<ProductCategoryDto>> GetAsync(string name);
        Task<OperationResultEnumerable<ProductCategoryDto>> GetAllAsync();
        Task<OperationResultEnumerable<ProductSubCategoryDto>> GetSubCategories(string categoryName);
        Task<OperationResult<ProductCategoryDto>> CreateAsync(CreateProductCategoryInput input);
        Task<OperationResult<ProductCategoryDto>> UpdateAsync(UpdateProductCategoryInput input);
        Task<OperationResult> DeleteAsync(DeleteProductCategoryInput input);
        Task<OperationResult> DeleteSubCategoryAsync(DeleteProductSubCategoryInput input);
        Task<OperationResult<ProductCategoryDto>> UpdateCategoryNameAsync(UpdateProductCategoryNameInput input);
        Task<OperationResult<ProductCategoryDto>> UpdateSubCategoryNameAsync(UpdateProductSubCategoryNameInput input);
    }
}
