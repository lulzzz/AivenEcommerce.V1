using AivenEcommerce.V1.Domain.Entities;

using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface ISaleRepository : IRepository<Sale, string>
    {
        Task<Sale> GetSaleAsync(Order order);
    }
}
