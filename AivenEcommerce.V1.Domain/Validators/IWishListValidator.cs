using System.Threading.Tasks;

using AivenEcommerce.V1.Application.Validations;
using AivenEcommerce.V1.Domain.Dtos.WishLists;

namespace AivenEcommerce.V1.Domain.Validators
{
    public interface IWishListValidator
    {
        Task<ValidationResult> ValidateAddProductToWishList(AddProductToWishListInput input);
        Task<ValidationResult> ValidateRemoveProductToWishList(RemoveProductToWishListInput input);
        Task<ValidationResult> ValidateRemoveAllWishList(RemoveAllWishListInput input);
        Task<ValidationResult> ValidateUpdateWishList(UpdateWishListInput input);
    }
}
