﻿using AivenEcommerce.V1.Domain.Shared.Common;
using AivenEcommerce.V1.Domain.Shared.Dtos.ProductCategories;
using AivenEcommerce.V1.Domain.Shared.OperationResults;

using System.Threading.Tasks;

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
