using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Entities;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface IProductOverviewRepository : IRepository<ProductOverview, Guid>
    {
        Task<ProductOverview> GetByProduct(Product product);
    }
}
