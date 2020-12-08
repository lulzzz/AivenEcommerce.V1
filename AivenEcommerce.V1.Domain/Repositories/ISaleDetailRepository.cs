using AivenEcommerce.V1.Domain.Entities;

using System;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface ISaleDetailRepository : IRepository<SaleDetail, Guid>
    {
        Task<SaleDetail> GetBySaleAsync(Sale sale);
    }
}
