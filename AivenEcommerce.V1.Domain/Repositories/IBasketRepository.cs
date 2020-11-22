using System;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Entities;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface IBasketRepository : IRepository<Basket, Guid>
    {
        Task<Basket> GetByCustomerAsync(string customerEmail);
    }
}
