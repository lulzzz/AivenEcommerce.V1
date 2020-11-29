using AivenEcommerce.V1.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface IProductVariantRepository : IRepository<ProductVariant, Guid>
    {
        Task<IEnumerable<ProductVariant>> GetByProduct(Product product);
        Task<ProductVariant> GetByNameAsync(Product product, string variantName);
    }
}
