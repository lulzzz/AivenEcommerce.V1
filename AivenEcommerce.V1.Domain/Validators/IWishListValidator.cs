using AivenEcommerce.V1.Application.Validations;
using AivenEcommerce.V1.Domain.Common;
using AivenEcommerce.V1.Domain.Dtos.WishLists;

using System.Threading.Tasks;

namespace AivenEcommerce.V1.Domain.Validators
{
    public interface IWishListValidator : IScopedService
    {
        Task<ValidationResult> ValidateAddProductToWishList(AddProductToWishListInput input);
        Task<ValidationResult> ValidateRemoveProductToWishList(RemoveProductToWishListInput input);
        Task<ValidationResult> ValidateRemoveAllWishList(RemoveAllWishListInput input);
        Task<ValidationResult> ValidateUpdateWishList(UpdateWishListInput input);
    }
}
