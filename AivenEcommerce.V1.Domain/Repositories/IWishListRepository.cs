using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Entities;

namespace AivenEcommerce.V1.Domain.Repositories
{
    public interface IWishListRepository
    {
        Task<WishList> GetByCustomerAsync(string customerEmail);
    }
}
