using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Entities;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface IProductVariantRepository : IRepository<ProductVariant, Guid>
    {
        Task<IEnumerable<ProductVariant>> GetByProduct(Product product);
        Task<ProductVariant> GetByNameAsync(Product product, string variantName);
    }
}
