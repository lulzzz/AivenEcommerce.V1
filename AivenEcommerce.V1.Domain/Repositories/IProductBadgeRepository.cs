using AivenEcommerce.V1.Domain.Entities;

using System;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface IProductBadgeRepository : IRepository<ProductBadge, Guid>
    {
        Task<ProductBadge> GetByProduct(Product product);
    }
}
