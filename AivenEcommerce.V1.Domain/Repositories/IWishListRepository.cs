using AivenEcommerce.V1.Domain.Entities;

using System;
using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface IWishListRepository : IRepository<WishList, Guid>
    {
        Task<WishList> GetByCustomerAsync(string customerEmail);
    }
}
