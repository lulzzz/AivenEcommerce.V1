using System.Threading.Tasks;

using AivenEcommerce.V1.Domain.Dtos.WishLists;
using AivenEcommerce.V1.Domain.OperationResults;

namespace AivenEcommerce.V1.Domain.Services
{
    public interface IWishListService
    {
        Task<OperationResult<WishListDto>> GetWishList(string customerEmail);
        Task<OperationResult<WishListDto>> AddProductToWishList(AddProductToWishListInput input);
        Task<OperationResult<WishListDto>> RemoveProductToWishList(RemoveProductToWishListInput input);
        Task<OperationResult<WishListDto>> RemoveAllWishList(RemoveAllWishListInput input);
        Task<OperationResult<WishListDto>> UpdateWishList(UpdateWishListInput input);
    }
}
