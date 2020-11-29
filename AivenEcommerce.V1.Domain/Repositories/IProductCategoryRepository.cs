using AivenEcommerce.V1.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface IProductCategoryRepository : IRepository<ProductCategory, Guid>
    {
        Task<ProductCategory> GetByNameAsync(string productCategoryName);
        Task<IEnumerable<ProductCategory>> GetAllNamesAsync();
    }
}
