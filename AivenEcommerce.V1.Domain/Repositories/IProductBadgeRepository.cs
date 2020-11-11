using System;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Entities;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface IProductBadgeRepository : IRepository<ProductBadge, Guid>
    {
        Task<ProductBadge> GetByProduct(Product product);
    }
}
