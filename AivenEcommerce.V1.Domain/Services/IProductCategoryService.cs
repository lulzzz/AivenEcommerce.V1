using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Dtos.ProductCategories;

using BusinessLogicEnterprise.Application.OperationResults;

namespace AivenEcommerce.V1.Domain.Services
{
    public interface IProductCategoryService
    {
        Task<OperationResult<ProductCategoryDto>> GetAsync(string name);
        Task<OperationResultEnumerable<ProductCategoryDto>> GetAllAsync();
        Task<OperationResult<ProductCategoryDto>> CreateAsync(CreateProductCategoryInput input);
        Task<OperationResult<ProductCategoryDto>> UpdateAsync(UpdateProductCategoryInput input);
        Task<OperationResult> DeleteAsync(DeleteProductCategoryInput input);
    }
}
