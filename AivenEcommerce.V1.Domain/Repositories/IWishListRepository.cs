using System;
using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Entities;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface IWishListRepository : IRepository<WishList, Guid>
    {
        Task<WishList> GetByCustomerAsync(string customerEmail);
    }
}
