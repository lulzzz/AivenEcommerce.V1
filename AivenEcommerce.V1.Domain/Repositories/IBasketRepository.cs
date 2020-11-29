using AivenEcommerce.V1.Domain.Entities;

using System;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface IBasketRepository : IRepository<Basket, Guid>
    {
        Task<Basket> GetByCustomerAsync(string customerEmail);
    }
}
